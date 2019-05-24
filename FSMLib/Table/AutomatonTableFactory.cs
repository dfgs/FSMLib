using FSMLib.Table.Actions;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		
		private void DevelopRuleDependencies(State<T> state, IEnumerable<ShiftOnNonTerminal<T>> Actions, IAutomatonTableFactoryContext<T> context, IEnumerable<Rule<T>> Rules, Rule<T> Axiom)
		{
			string[] reductionDependencies;
			Segment<T> dependentSegment;

			foreach (ShiftOnNonTerminal<T> nonTerminalAction in Actions)
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
			T[] nextInputs;
			string[] reductionDependencies;

			foreach (ShiftOnNonTerminal<T> nonTerminalAction in Actions)
			{
				nextInputs = context.GetFirstTerminalsAfterAction( nonTerminalAction).ToArray();

				reductionDependencies = context.GetRuleReductionDependency(Rules, nonTerminalAction.Name).ToArray();
				foreach (string reductionDepency in reductionDependencies)
				{
					foreach (Reduce<T> reductionAction in context.GetReductionActions(reductionDepency))
					{
						foreach (T value in nextInputs)
						{
							reductionAction.Targets.Add(new ReductionTarget<T>() { TargetStateIndex = context.GetStateIndex(state), Value = value });
						}
					}

				}
			}
		}
		public AutomatonTable<T> BuildAutomatonTable(IEnumerable<Rule<T>> Rules, IEnumerable<T> Alphabet)
		{
			AutomatonTable<T> automatonTable;
			State<T> root;
			Segment<T> segment;
			AutomatonTableFactoryContext<T> context;
			Rule<T> axiom;
			Rule<T>[] rules;
			BaseAction<T>[] actions;


			if (Rules == null) throw new System.ArgumentNullException("Rules");
			if (Alphabet == null) throw new System.ArgumentNullException("Alphabet");

	

			automatonTable = new AutomatonTable<T>();
			automatonTable.Alphabet.AddRange(Alphabet);

			if (!Rules.Any()) return automatonTable;

			rules = Rules.ToArray();// mandatory in order to fix cache issue when using enumeration
			axiom = rules.First();
			
			context = new AutomatonTableFactoryContext<T>(segmentFactoryProvider, automatonTable);
			root = context.CreateState();
			
			// build all segments from rules
			foreach(Rule<T> rule in rules)
			{
				if (rule==axiom)
				{
					actions = new BaseAction<T>[]
					{
						new Reduce<T>() {    Name=rule.Name},
						new Accept<T>() { Name=rule.Name}
					};
				}
				else
				{
					actions = new BaseAction<T>[]
					{
						new Reduce<T>() {   Name=rule.Name}
					};
				}
				segment = context.BuildSegment( rule, actions  );
				context.Connect(root.AsEnumerable(), segment.Actions);
			}

			// develop rule dependencies
			foreach (State<T> state in automatonTable.States)
			{
				if (state == root) continue;
				DevelopRuleDependencies(state, state.NonTerminalActions.ToArray(), context, rules, axiom);
			}//*/
			 // add reduction action to root states
			foreach (State<T> state in automatonTable.States)
			{
				if (state == root) continue;
				CompleteReductionTargets(state, state.NonTerminalActions.ToArray(), context, rules, axiom);
			}//*/


			return automatonTable;
		}


		private AutomatonTableTuple<T> GetNextTuple(AutomatonTableFactoryContext<T> Context, Stack<AutomatonTableTuple<T>> OpenList,List<AutomatonTableTuple<T>> SituationMapping, IEnumerable<Situation<T>> NextSituations)
		{
			AutomatonTableTuple<T> nextTuple;

			nextTuple = SituationMapping.FirstOrDefault(item => item.Situations.IsIndenticalToEx(NextSituations));
			if (nextTuple == null)
			{
				nextTuple = new AutomatonTableTuple<T>();
				nextTuple.State = Context.CreateState();
				nextTuple.Situations = NextSituations;

				nextTuple.State.ReductionActions.AddRange(NextSituations.SelectMany(item => item.AutomatonTable.States[item.StateIndex].ReductionActions));
				nextTuple.State.AcceptActions.AddRange(NextSituations.SelectMany(item => item.AutomatonTable.States[item.StateIndex].AcceptActions));

				SituationMapping.Add(nextTuple);
				OpenList.Push(nextTuple);
			}

			return nextTuple;
		}
		public AutomatonTable<T> BuildDeterministicAutomatonTable(AutomatonTable<T> BaseAutomatonTable)
		{
			IEnumerable<Situation<T>> nextSituations;
			AutomatonTable<T> automatonTable;
			List<AutomatonTableTuple<T>> situationMapping;
			AutomatonTableTuple<T> currentTuple,nextTuple;
			Stack<AutomatonTableTuple<T>> openList;
			Shift<T> action;
			AutomatonTableFactoryContext<T> context;
			ReductionTarget<T>[] targets;

			if (BaseAutomatonTable == null) throw new System.ArgumentNullException("BaseAutomatonTable");

			automatonTable = new AutomatonTable<T>();
			automatonTable.Alphabet.AddRange(BaseAutomatonTable.Alphabet);

			if (BaseAutomatonTable.States.Count == 0) return automatonTable;
			context = new AutomatonTableFactoryContext<T>(segmentFactoryProvider,automatonTable);

			situationMapping = new List<AutomatonTableTuple<T>>();
			openList = new Stack<AutomatonTableTuple<T>>();

			currentTuple= GetNextTuple(context, openList, situationMapping, new Situation<T>() { AutomatonTable = BaseAutomatonTable, StateIndex = 0 }.AsEnumerable());
	
			while (openList.Count>0)
			{
				currentTuple = openList.Pop();
				foreach (T value in situationProducer.GetNextTerminals(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(currentTuple.Situations, value);
					// do we have the same situation list in automatonTable ?
					nextTuple = GetNextTuple(context,openList,situationMapping,nextSituations);
					// if not we push this situation list in processing stack
					action = new ShiftOnTerminal<T>() { Value = value, TargetStateIndex = context.GetStateIndex(nextTuple.State) };
					context.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}
				foreach (string name in situationProducer.GetNextNonTerminals(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(currentTuple.Situations, name);
					// do we have the same situation list in automatonTable ?
					nextTuple = GetNextTuple(context, openList, situationMapping, nextSituations);
					// if not we push this situation list in processing stack
					action = new ShiftOnNonTerminal<T>() { Name = name, TargetStateIndex = context.GetStateIndex(nextTuple.State) };
					context.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}

			}

			// translate reduction actions, because indices in base automatonTable are not the same in det automatonTable
			foreach (Reduce<T> reductionAction in automatonTable.States.SelectMany(item=>item.ReductionActions))
			{
				targets = reductionAction.Targets.ToArray();
				reductionAction.Targets.Clear();
				foreach(ReductionTarget<T> target in targets)
				{
					nextTuple = situationMapping.FirstOrDefault(item => item.Situations.FirstOrDefault(situation => situation.StateIndex == target.TargetStateIndex) != null);
					if (nextTuple == null) throw new Exception("Failed to translate reduction actions");
					reductionAction.Targets.Add(new ReductionTarget<T>() { TargetStateIndex = context.GetStateIndex(nextTuple.State), Value = target.Value }); ;
				}
			}

			return automatonTable;
		}









	}

}
