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
		private ISituationProducer<T> situationProducer;

		public AutomatonTableFactory(  ISituationProducer<T> SituationProducer)
		{
			if (SituationProducer == null) throw new ArgumentNullException("SituationProducer");
			this.situationProducer = SituationProducer;
		}


		private void AddReductions(State<T> State,ISituationCollection<T> Situations)
		{
			Reduce<T> reduce;

			foreach(Situation<T> situation in Situations.GetReductionSituations())
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
			ISituationCollection<T> nextSituations,developpedSituations;
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

			developpedSituations = graph.Develop( new Situation<T>() { Rule = acceptRule, Predicate = nonTerminal }.AsEnumerable());
			developpedSituations.Add(new Situation<T>() { Rule = acceptRule, Predicate = nonTerminal });
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
				foreach (BaseTerminalInput<T> input in situationProducer.GetNextTerminalInputs(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(graph, currentTuple.Situations, input);
					developpedSituations = graph.Develop( nextSituations);
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
					situationProducer.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}

				foreach (NonTerminalInput<T> input in situationProducer.GetNextNonTerminalInputs(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(graph, currentTuple.Situations, input);
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
					action = new ShiftOnNonTerminal<T>() { Name = input.Name, TargetStateIndex = automatonTable.States.IndexOf(nextTuple.State) };
					situationProducer.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}

			}

		


			return automatonTable;
		}









	}

}
