using FSMLib.Actions;
using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Situations;

namespace FSMLib.Table
{
	public class AutomatonTableFactory<T> : IAutomatonTableFactory<T>
	{
		//private ISituationProducer<T> situationProducer;

		public AutomatonTableFactory( )
		{
		}

		private void Connect(IEnumerable<State<T>> States, IEnumerable<Shift<T>> Actions)
		{

			if (States == null) throw new ArgumentNullException("States");
			if (Actions == null) throw new ArgumentNullException("Actions");

			foreach (State<T> state in States)
			{
				foreach (Shift<T> action in Actions)
				{
					switch (action)
					{
						case ShiftOnTerminal<T> tr:
							if (state.TerminalActions.FirstOrDefault(item => item.Equals(tr)) == null) state.TerminalActions.Add(tr);
							break;
						case ShiftOnNonTerminal<T> tr:
							if (state.NonTerminalActions.FirstOrDefault(item => item.Equals(tr)) == null) state.NonTerminalActions.Add(tr);
							break;
						default:
							throw (new NotImplementedException("Invalid action type"));
					}
				}
			}
		}

		public IEnumerable<NonTerminalInput<T>> GetNextNonTerminalInputs(IEnumerable<Situation<T>> Situations)
		{
			if (Situations == null) throw new ArgumentNullException("Situations");
			return Situations.Select(item => item.Predicate.GetInput()).OfType<NonTerminalInput<T>>().DistinctEx();
		}
		public IEnumerable<BaseTerminalInput<T>> GetNextTerminalInputs(IEnumerable<Situation<T>> Situations)
		{
			if (Situations == null) throw new ArgumentNullException("Situations");
			return Situations.Select(item => item.Predicate.GetInput()).OfType<BaseTerminalInput<T>>().Where(item => !(item is ReduceInput<T>)).DistinctEx();
		}

		private void AddReductions(State<T> State,ISituationCollection<T> Situations)
		{
			Reduce<T> reduce;

			if (State == null) throw new ArgumentNullException("State");
			if (Situations == null) throw new ArgumentNullException("Situations");

			foreach (Situation<T> situation in Situations.GetReductionSituations())
			{
				reduce = new Reduce<T>();
				reduce.Name = situation.Rule.Name;
				reduce.Input = situation.Input;

				State.ReductionActions.Add(reduce);
			}
		}
	

		public AutomatonTable<T> BuildAutomatonTable(IEnumerable<Rule<T>> Rules, IEnumerable<T> Alphabet)
		{
			Rule<T> axiom,acceptRule;
			Sequence<T> sequence;
			NonTerminal<T> nonTerminal;
			SituationGraph<T> graph;
			Rule<T>[] rules;

			SituationDictionary<T> situationDictionary;
			ISituationCollection<T> developpedSituations;
			IEnumerable<Situation<T>> nextSituations;
			AutomatonTable<T> automatonTable;
			
			AutomatonTableTuple<T> currentTuple,nextTuple;
			Stack<AutomatonTableTuple<T>> openList;
			Shift<T> action;

			State<T> state;

			if (Rules == null) throw new System.ArgumentNullException("Rules");
			if (Alphabet == null) throw new System.ArgumentNullException("Alphabet");

			// needed to fix predicate dynamic creation due to enumeration
			rules = Rules.ToArray();

			automatonTable = new AutomatonTable<T>();
			automatonTable.Alphabet.AddRange(Alphabet);


			axiom = rules.FirstOrDefault();
			if (axiom == null) return automatonTable;

			nonTerminal = new NonTerminal<T>() { Name = axiom.Name };
			sequence = new Sequence<T>();
			sequence.Items.Add(nonTerminal);
			sequence.Items.Add(new EOS<T>() );

			acceptRule = new Rule<T>() {Name="Axiom" };
			acceptRule.Predicate = sequence;

			graph = new SituationGraph<T>( acceptRule.AsEnumerable().Concat(rules) );

			situationDictionary = new SituationDictionary<T>();
			openList = new Stack<AutomatonTableTuple<T>>();

			developpedSituations = new SituationCollection<T>();
			developpedSituations.AddRange( graph.GetRootSituations() );

			nextTuple = situationDictionary.GetTuple(developpedSituations);
			if (nextTuple == null)
			{
				state = new State<T>();
				AddReductions(state, developpedSituations);
				automatonTable.States.Add(state);
				nextTuple = situationDictionary.CreateTuple(state,developpedSituations);
				openList.Push(nextTuple);
			}
				
			
	
			while (openList.Count>0)
			{
				currentTuple = openList.Pop();
				foreach (BaseTerminalInput<T> input in GetNextTerminalInputs(currentTuple.Situations))
				{
					nextSituations = currentTuple.Situations.Where(item => item.Predicate.GetInput().Match(input)).SelectMany(item => graph.GetNextSituations(item));
					developpedSituations = new SituationCollection<T>();
					developpedSituations.AddRange(nextSituations);

					nextTuple = situationDictionary.GetTuple(developpedSituations);
					if (nextTuple == null)
					{
						state = new State<T>();
						AddReductions(state, developpedSituations);
						automatonTable.States.Add(state);
						nextTuple = situationDictionary.CreateTuple(state, developpedSituations);
						openList.Push(nextTuple);
					}
					action = new ShiftOnTerminal<T>() { Input = input, TargetStateIndex = automatonTable.States.IndexOf(nextTuple.State) };
					Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}

				foreach (NonTerminalInput<T> input in GetNextNonTerminalInputs(currentTuple.Situations))
				{
					nextSituations = currentTuple.Situations.Where(item => item.Predicate.GetInput().Match(input)).SelectMany(item => graph.GetNextSituations(item));
					developpedSituations = new SituationCollection<T>();
					developpedSituations.AddRange(nextSituations);

					nextTuple = situationDictionary.GetTuple(developpedSituations);
					if (nextTuple == null)
					{
						state = new State<T>();
						AddReductions(state, developpedSituations);
						automatonTable.States.Add(state);
						nextTuple = situationDictionary.CreateTuple(state, developpedSituations);
						openList.Push(nextTuple);
					}
					action = new ShiftOnNonTerminal<T>() { Name = input.Name, TargetStateIndex = automatonTable.States.IndexOf(nextTuple.State) };
					Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}

			}

		


			return automatonTable;
		}









	}

}
