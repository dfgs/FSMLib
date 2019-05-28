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



		private AutomatonTableTuple<T> GetNextTuple(ISituationGraph<T> SituationGraph, AutomatonTable<T> Table, Stack<AutomatonTableTuple<T>> OpenList,List<AutomatonTableTuple<T>> SituationMapping, IEnumerable<Situation2<T>> NextSituations)
		{
			AutomatonTableTuple<T> nextTuple;

			nextTuple = SituationMapping.FirstOrDefault(item => item.Situations.IsIndenticalToEx(NextSituations));
			if (nextTuple == null)
			{
				nextTuple = new AutomatonTableTuple<T>();
				nextTuple.State = new State<T>();
				nextTuple.Situations = NextSituations;

				Table.States.Add(nextTuple.State);

				foreach(Situation2<T> situation in NextSituations.Where(item=>item.Predicate==ReducePredicate<T>.Instance))
				{
					nextTuple.State.ReductionActions.Add(new Reduce<T>() { Name=situation.Rule.Name});
				}
				//nextTuple.State.ReductionActions.AddRange(NextSituations.SelectMany(item => item.AutomatonTable.States[item.StateIndex].ReductionActions));
				//nextTuple.State.AcceptActions.AddRange(NextSituations.SelectMany(item => item.AutomatonTable.States[item.StateIndex].AcceptActions));

				SituationMapping.Add(nextTuple);
				OpenList.Push(nextTuple);
			}

			return nextTuple;
		}

		public AutomatonTable<T> BuildAutomatonTable(IEnumerable<Rule<T>> Rules, IEnumerable<T> Alphabet)
		{
			Rule<T> axiom,acceptRule;
			Sequence<T> sequence;
			NonTerminal<T> nonTerminal;
			SituationGraph<T> graph;
			Rule<T>[] rules;

			IEnumerable<Situation2<T>> nextSituations,developpedSituations;
			AutomatonTable<T> automatonTable;
			List<AutomatonTableTuple<T>> situationMapping;
			AutomatonTableTuple<T> currentTuple,nextTuple;
			Stack<AutomatonTableTuple<T>> openList;
			Shift<T> action;
			//ReductionTarget<T>[] targets;


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
			//sequence.Items.Add(new Terminal<T>() { Value=new  } );

			acceptRule = new Rule<T>() {Name="Axiom" };
			acceptRule.Predicate = sequence;

			graph = new SituationGraph<T>( acceptRule.AsEnumerable().Concat(rules) );
	
			situationMapping = new List<AutomatonTableTuple<T>>();
			openList = new Stack<AutomatonTableTuple<T>>();

			developpedSituations = situationProducer.Develop(graph, new Situation2<T>() { Rule = acceptRule, Predicate = nonTerminal }.AsEnumerable(),rules);
			currentTuple = GetNextTuple(graph, automatonTable, openList, situationMapping, developpedSituations) ;
	
			while (openList.Count>0)
			{
				currentTuple = openList.Pop();
				foreach (BaseTerminalInput<T> input in situationProducer.GetNextTerminalInputs(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(graph,currentTuple.Situations, input);
					developpedSituations = situationProducer.Develop(graph,nextSituations, rules);
					// do we have the same situation list in automatonTable ?
					nextTuple = GetNextTuple(graph, automatonTable,openList,situationMapping,developpedSituations);
					// if not we push this situation list in processing stack
					action = new ShiftOnTerminal<T>() { Input = input, TargetStateIndex = automatonTable.States.IndexOf(nextTuple.State) };
					situationProducer.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}
				foreach (string name in situationProducer.GetNextNonTerminals(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(graph, currentTuple.Situations, new NonTerminalInput<T>() {Name=name } );
					developpedSituations = situationProducer.Develop(graph, nextSituations, rules);
					// do we have the same situation list in automatonTable ?
					nextTuple = GetNextTuple(graph, automatonTable, openList, situationMapping, developpedSituations);
					// if not we push this situation list in processing stack
					action = new ShiftOnNonTerminal<T>() { Name = name, TargetStateIndex = automatonTable.States.IndexOf(nextTuple.State) };
					situationProducer.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}

			}

			
			return automatonTable;
		}









	}

}
