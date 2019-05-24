using FSMLib.ActionTables.Actions;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables
{
	public class ActionTableFactory<T> : IActionTableFactory<T>
	{
		private ISegmentFactoryProvider<T> segmentFactoryProvider;
		private ISituationProducer<T> situationProducer;

		public ActionTableFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider, ISituationProducer<T> SituationProducer)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (SituationProducer == null) throw new ArgumentNullException("SituationProducer");
			this.situationProducer = SituationProducer;
		}

		
		private void DevelopRuleDependencies(State<T> state, IEnumerable<ShifOnNonTerminal<T>> Actions, IActionTableFactoryContext<T> context, IEnumerable<Rule<T>> Rules, Rule<T> Axiom)
		{
			string[] reductionDependencies;
			Segment<T> dependentSegment;

			foreach (ShifOnNonTerminal<T> nonTerminalAction in Actions)
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
		private void CompleteReductionTargets(State<T> state, IEnumerable<ShifOnNonTerminal<T>> Actions, IActionTableFactoryContext<T> context, IEnumerable<Rule<T>> Rules, Rule<T> Axiom)
		{
			T[] nextInputs;
			string[] reductionDependencies;

			foreach (ShifOnNonTerminal<T> nonTerminalAction in Actions)
			{
				nextInputs = context.GetFirstTerminalsAfterAction(state, nonTerminalAction.Name).ToArray();

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
		public ActionTable<T> BuildActionTable(IEnumerable<Rule<T>> Rules, IEnumerable<T> Alphabet)
		{
			ActionTable<T> actionTable;
			State<T> root;
			Segment<T> segment;
			ActionTableFactoryContext<T> context;
			Rule<T> axiom;
			Rule<T>[] rules;
			BaseAction<T>[] actions;


			if (Rules == null) throw new System.ArgumentNullException("Rules");
			if (Alphabet == null) throw new System.ArgumentNullException("Alphabet");

	

			actionTable = new ActionTable<T>();
			actionTable.Alphabet.AddRange(Alphabet);

			if (!Rules.Any()) return actionTable;

			rules = Rules.ToArray();// mandatory in order to fix cache issue when using enumeration
			axiom = rules.First();
			
			context = new ActionTableFactoryContext<T>(segmentFactoryProvider, actionTable);
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
			foreach (State<T> state in actionTable.States)
			{
				if (state == root) continue;
				DevelopRuleDependencies(state, state.NonTerminalActions.ToArray(), context, rules, axiom);
			}//*/
			 // add reduction action to root states
			foreach (State<T> state in actionTable.States)
			{
				if (state == root) continue;
				CompleteReductionTargets(state, state.NonTerminalActions.ToArray(), context, rules, axiom);
			}//*/


			return actionTable;
		}


		private ActionTableTuple<T> GetNextTuple(ActionTableFactoryContext<T> Context, Stack<ActionTableTuple<T>> OpenList,List<ActionTableTuple<T>> SituationMapping, IEnumerable<Situation<T>> NextSituations)
		{
			ActionTableTuple<T> nextTuple;

			nextTuple = SituationMapping.FirstOrDefault(item => item.Situations.IsIndenticalToEx(NextSituations));
			if (nextTuple == null)
			{
				nextTuple = new ActionTableTuple<T>();
				nextTuple.State = Context.CreateState();
				nextTuple.Situations = NextSituations;

				nextTuple.State.ReductionActions.AddRange(NextSituations.SelectMany(item => item.ActionTable.States[item.StateIndex].ReductionActions));
				nextTuple.State.AcceptActions.AddRange(NextSituations.SelectMany(item => item.ActionTable.States[item.StateIndex].AcceptActions));

				SituationMapping.Add(nextTuple);
				OpenList.Push(nextTuple);
			}

			return nextTuple;
		}
		public ActionTable<T> BuildDeterministicActionTable(ActionTable<T> BaseActionTable)
		{
			IEnumerable<Situation<T>> nextSituations;
			ActionTable<T> actionTable;
			List<ActionTableTuple<T>> situationMapping;
			ActionTableTuple<T> currentTuple,nextTuple;
			Stack<ActionTableTuple<T>> openList;
			Shift<T> action;
			ActionTableFactoryContext<T> context;
			ReductionTarget<T>[] targets;

			if (BaseActionTable == null) throw new System.ArgumentNullException("BaseActionTable");

			actionTable = new ActionTable<T>();
			actionTable.Alphabet.AddRange(BaseActionTable.Alphabet);

			if (BaseActionTable.States.Count == 0) return actionTable;
			context = new ActionTableFactoryContext<T>(segmentFactoryProvider,actionTable);

			situationMapping = new List<ActionTableTuple<T>>();
			openList = new Stack<ActionTableTuple<T>>();

			currentTuple= GetNextTuple(context, openList, situationMapping, new Situation<T>() { ActionTable = BaseActionTable, StateIndex = 0 }.AsEnumerable());
	
			while (openList.Count>0)
			{
				currentTuple = openList.Pop();
				foreach (T value in situationProducer.GetNextTerminals(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(currentTuple.Situations, value);
					// do we have the same situation list in actionTable ?
					nextTuple = GetNextTuple(context,openList,situationMapping,nextSituations);
					// if not we push this situation list in processing stack
					action = new ShiftOnTerminal<T>() { Value = value, TargetStateIndex = context.GetStateIndex(nextTuple.State) };
					context.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}
				foreach (string name in situationProducer.GetNextNonTerminals(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(currentTuple.Situations, name);
					// do we have the same situation list in actionTable ?
					nextTuple = GetNextTuple(context, openList, situationMapping, nextSituations);
					// if not we push this situation list in processing stack
					action = new ShifOnNonTerminal<T>() { Name = name, TargetStateIndex = context.GetStateIndex(nextTuple.State) };
					context.Connect(currentTuple.State.AsEnumerable(), action.AsEnumerable());
				}

			}

			// translate reduction actions, because indices in base actionTable are not the same in det actionTable
			foreach (Reduce<T> reductionAction in actionTable.States.SelectMany(item=>item.ReductionActions))
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

			return actionTable;
		}









	}

}
