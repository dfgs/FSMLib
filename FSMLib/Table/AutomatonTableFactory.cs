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
					//if (state.ShiftActions.FirstOrDefault(item => item.Equals(action)) == null)
					state.ShiftActions.Add(action);
				}
			}
		}

	
		private IEnumerable<IInput<T>> GetNextInputs(IEnumerable<Situation<T>> Situations)
		{
			if (Situations == null) throw new ArgumentNullException("Situations");
			return Situations.SelectMany(item => item.Predicate.GetInputs()).DistinctEx();
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
	
		private IEnumerable<Situation<T>> CreateNextSituations(ISituationGraph<T> Graph,BaseInput<T> Input,IEnumerable<Situation<T>> CurrentSituations)
		{
			IEnumerable<Situation<T>> matchingSituations;

			matchingSituations = CurrentSituations.Where(s => s.Predicate.GetInputs().FirstOrDefault( i=> i.Match(Input))!=null);

			foreach(Situation<T> situation in matchingSituations)
			{
				foreach(Situation<T> nextSituation in Graph.GetNextSituations(situation))
				{
					yield return nextSituation;
				}
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

			nextSituations = new Situation<T>[] { new Situation<T>() { Rule=acceptRule, Predicate=nonTerminal } };
			developpedSituations = graph.Develop(nextSituations);

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
				foreach (BaseInput<T> input in GetNextInputs(currentTuple.Situations))
				{
					nextSituations = CreateNextSituations(graph, input, currentTuple.Situations);
					developpedSituations = graph.Develop(nextSituations);

					nextTuple = situationDictionary.GetTuple(developpedSituations);
					if (nextTuple == null)
					{
						state = new State<T>();
						AddReductions(state, developpedSituations);
						automatonTable.States.Add(state);
						nextTuple = situationDictionary.CreateTuple(state, developpedSituations);
						openList.Push(nextTuple);
					}
					action = new Shift<T>() { Input = input, TargetStateIndex = automatonTable.States.IndexOf(nextTuple.State) };
					Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}
				

			}


			return automatonTable;
		}









	}

}
