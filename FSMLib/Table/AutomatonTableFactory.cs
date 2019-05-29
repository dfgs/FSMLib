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

			developpedSituations = situationProducer.Develop(graph, new Situation<T>() { Rule = acceptRule, Predicate = nonTerminal }.AsEnumerable(),rules);
			nextTuple = situationDictionary.GetTuple(developpedSituations);
			if (nextTuple == null)
			{
				nextTuple = situationDictionary.CreateTuple(developpedSituations);
				automatonTable.States.Add(nextTuple.State);
				openList.Push(nextTuple);
			}
				
	
			while (openList.Count>0)
			{
				currentTuple = openList.Pop();
				foreach (BaseTerminalInput<T> input in situationProducer.GetNextTerminalInputs(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(graph, currentTuple.Situations, input);
					developpedSituations = situationProducer.Develop(graph, nextSituations, rules);
					nextTuple = situationDictionary.GetTuple(developpedSituations);
					if (nextTuple == null)
					{
						nextTuple = situationDictionary.CreateTuple(developpedSituations);
						automatonTable.States.Add(nextTuple.State);
						openList.Push(nextTuple);
					}
					action = new ShiftOnTerminal<T>() { Input = input, TargetStateIndex = automatonTable.States.IndexOf(nextTuple.State) };
					situationProducer.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}

				foreach (NonTerminalInput<T> input in situationProducer.GetNextNonTerminalInputs(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(graph, currentTuple.Situations, input);
					developpedSituations = situationProducer.Develop(graph, nextSituations, rules);
					nextTuple = situationDictionary.GetTuple(developpedSituations);
					if (nextTuple == null)
					{
						nextTuple = situationDictionary.CreateTuple(developpedSituations);
						automatonTable.States.Add(nextTuple.State);
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
