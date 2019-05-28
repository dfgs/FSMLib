using FSMLib.Actions;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
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
		private ISegmentFactoryProvider<T> segmentFactoryProvider;
		private ISituationProducer<T> situationProducer;

		public AutomatonTableFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider, ISituationProducer<T> SituationProducer)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (SituationProducer == null) throw new ArgumentNullException("SituationProducer");
			this.situationProducer = SituationProducer;
		}

		
		private void DevelopRuleDependencies(State<T> state, IAutomatonTableFactoryContext<T> context, IEnumerable<Rule<T>> Rules)
		{
			string[] reductionDependencies;
			Segment<T> dependentSegment;

			foreach (ShiftOnNonTerminal<T> nonTerminalAction in state.NonTerminalActions.ToArray())
			{
				reductionDependencies = context.GetRuleReductionDependency(Rules, nonTerminalAction.Name).ToArray();
				foreach(string reductionDepency in reductionDependencies)
				{
					foreach (Rule<T> dependentRule in Rules.Where(item => item.Name == reductionDepency))
					{
						dependentSegment = context.BuildSegment(dependentRule, Enumerable.Empty<BaseAction<T>>());
						context.Connect(state.AsEnumerable(), dependentSegment.Actions);
					}
				}
			}
		}

		private void CompleteReductionTargets(State<T> state, IEnumerable<ShiftOnNonTerminal<T>> Actions, IAutomatonTableFactoryContext<T> context, IEnumerable<Rule<T>> Rules, Rule<T> Axiom)
		{
			BaseTerminalInput<T>[] nextInputs;
			string[] reductionDependencies;
			Segment<T> dependentSegment;
			int index;

			index = context.GetStateIndex(state);

			// enumerate all non terminal transitions in node
			foreach (ShiftOnNonTerminal<T> nonTerminalAction in Actions)
			{
				// get all terminals following this non terminal transition
				nextInputs = context.GetFirstTerminalInputsAfterAction( nonTerminalAction).Concat(state.ReductionActions.SelectMany(item=>item.Targets.Select(item2=>item2.Input))).ToArray();

				// get all rules that can reduce to this transition
				reductionDependencies = context.GetRuleReductionDependency(Rules, nonTerminalAction.Name).ToArray();
				foreach (string reductionDepency in reductionDependencies)
				{
					foreach (Rule<T> dependentRule in Rules.Where(item => item.Name == reductionDepency))
					{
						dependentSegment = context.BuildSegment(dependentRule, Enumerable.Empty<BaseAction<T>>());
						foreach(Reduce<T> reductionAction in dependentSegment.Outputs.SelectMany(item=>item.ReductionActions))
						{
							foreach (BaseTerminalInput<T> input in nextInputs)
							{
								reductionAction.Targets.Add(new ReductionTarget<T>() { TargetStateIndex=index,Input=input } );
							}
						}
					}
				}
			}
		}


		private AutomatonTableTuple<T> GetNextTuple(AutomatonTable<T> Table, Stack<AutomatonTableTuple<T>> OpenList,List<AutomatonTableTuple<T>> SituationMapping, IEnumerable<Situation2<T>> NextSituations)
		{
			AutomatonTableTuple<T> nextTuple;

			nextTuple = SituationMapping.FirstOrDefault(item => item.Situations.IsIndenticalToEx(NextSituations));
			if (nextTuple == null)
			{
				nextTuple = new AutomatonTableTuple<T>();
				nextTuple.State = new State<T>();
				nextTuple.Situations = NextSituations;

				Table.States.Add(nextTuple.State);

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

			IEnumerable<Situation2<T>> nextSituations,developpedSituations;
			AutomatonTable<T> automatonTable;
			List<AutomatonTableTuple<T>> situationMapping;
			AutomatonTableTuple<T> currentTuple,nextTuple;
			Stack<AutomatonTableTuple<T>> openList;
			Shift<T> action;
			//ReductionTarget<T>[] targets;


			if (Rules == null) throw new System.ArgumentNullException("Rules");
			if (Alphabet == null) throw new System.ArgumentNullException("Alphabet");

			automatonTable = new AutomatonTable<T>();
			automatonTable.Alphabet.AddRange(Alphabet);


			axiom = Rules.FirstOrDefault();
			if (axiom == null) return automatonTable;

			nonTerminal = new NonTerminal<T>() { Name = axiom.Name };
			sequence = new Sequence<T>();
			sequence.Items.Add(nonTerminal);
			//sequence.Items.Add(new Terminal<T>() { Value=new  } );

			acceptRule = new Rule<T>() {Name="Axiom" };
			acceptRule.Predicate = sequence;

			graph = new SituationGraph<T>( sequence.AsEnumerable().Concat(Rules.Select(item=>item.Predicate)) );
	
			situationMapping = new List<AutomatonTableTuple<T>>();
			openList = new Stack<AutomatonTableTuple<T>>();

			developpedSituations = situationProducer.Develop(graph, new Situation2<T>() { Rule = acceptRule, Predicate = nonTerminal }.AsEnumerable(),Rules);
			currentTuple = GetNextTuple(automatonTable, openList, situationMapping, developpedSituations);
	
			while (openList.Count>0)
			{
				currentTuple = openList.Pop();
				foreach (BaseTerminalInput<T> input in situationProducer.GetNextTerminalInputs(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(graph,currentTuple.Situations, input);
					developpedSituations = situationProducer.Develop(graph,nextSituations, Rules);
					// do we have the same situation list in automatonTable ?
					nextTuple = GetNextTuple(automatonTable,openList,situationMapping,developpedSituations);
					// if not we push this situation list in processing stack
					action = new ShiftOnTerminal<T>() { Input = input, TargetStateIndex = automatonTable.States.IndexOf(nextTuple.State) };
					situationProducer.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}
				foreach (string name in situationProducer.GetNextNonTerminals(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(graph, currentTuple.Situations, new NonTerminalInput<T>() {Name=name } );
					developpedSituations = situationProducer.Develop(graph, nextSituations, Rules);
					// do we have the same situation list in automatonTable ?
					nextTuple = GetNextTuple(automatonTable, openList, situationMapping, developpedSituations);
					// if not we push this situation list in processing stack
					action = new ShiftOnNonTerminal<T>() { Name = name, TargetStateIndex = automatonTable.States.IndexOf(nextTuple.State) };
					situationProducer.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}

			}

			
			return automatonTable;
		}









	}

}
