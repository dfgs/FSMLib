using FSMLib.Table.Actions;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public class AutomatonTableFactoryContext<T> : IAutomatonTableFactoryContext<T>
	{
		private List<Rule<T>> compilingList;
		private List<string> openList;

		private AutomatonTable<T> automatonTable;
		private ISegmentFactoryProvider<T> segmentFactoryProvider;
		private Dictionary<Rule<T>, Segment<T>> cache;


		public AutomatonTableFactoryContext(ISegmentFactoryProvider<T> SegmentFactoryProvider, AutomatonTable<T> AutomatonTable)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (AutomatonTable == null) throw new ArgumentNullException("AutomatonTable");
			this.automatonTable = AutomatonTable;

			this.cache = new Dictionary<Rule<T>, Segment<T>>();

			compilingList = new List<Rule<T>>();
			openList = new List<string>();
		}


		public Segment<T> BuildSegment(Rule<T> Rule, IEnumerable<BaseAction<T>> OutActions)
		{
			ISegmentFactory<T> segmentFactory;
			Segment<T> segment;

			if (cache.TryGetValue(Rule, out segment))
			{
				return segment;
			}

			if (compilingList.Contains(Rule)) throw new InvalidOperationException("Recursive rule call");
			compilingList.Add(Rule);
			segmentFactory = segmentFactoryProvider.GetSegmentFactory(Rule.Predicate);


			segment = segmentFactory.BuildSegment(this, Rule.Predicate, OutActions);
			cache.Add(Rule, segment);
			compilingList.Remove(Rule);

			return segment;
		}
		public void Connect(IEnumerable<State<T>> States, IEnumerable<BaseAction<T>> Actions)
		{

			if (States == null) throw new ArgumentNullException("States");
			if (Actions == null) throw new ArgumentNullException("Actions");

			foreach (State<T> state in States)
			{
				foreach (BaseAction<T> action in Actions)
				{
					switch (action)
					{
						case ShiftOnTerminal<T> tr:
							if (state.TerminalActions.FirstOrDefault(item=>item.Equals(tr))==null) state.TerminalActions.Add(tr);
							break;
						case ShiftOnNonTerminal<T> tr:
							if (state.NonTerminalActions.FirstOrDefault(item => item.Equals(tr)) == null) state.NonTerminalActions.Add(tr);
							break;
						case Reduce<T> tr:
							if (state.ReductionActions.FirstOrDefault(item => item.Equals(tr)) == null) state.ReductionActions.Add(tr);
							break;
						case Accept<T> tr:
							if (state.AcceptActions.FirstOrDefault(item => item.Equals(tr)) == null) state.AcceptActions.Add(tr);
							break;

						default:
							throw (new NotImplementedException("Invalid action type"));
					}

				}
			}
		}


		public State<T> GetTargetState(int Index)
		{
			if ((Index < 0) || (Index >= automatonTable.States.Count)) throw (new IndexOutOfRangeException("State index is out of range"));
			return automatonTable.States[Index];
		}

		public State<T> CreateState()
		{
			State<T> state;
			state = new State<T>();
			//state.Name = automatonTable.States.Count.ToString();
			automatonTable.States.Add(state);
			return state;
		}

		public int GetStateIndex(State<T> State)
		{
			return automatonTable.States.IndexOf(State);
		}

		public IEnumerable<T> GetAlphabet()
		{
			return automatonTable.Alphabet;
		}


		public IEnumerable<T> GetFirstTerminalsAfterAction(State<T> State, string Name)
		{
			State<T> nextState;
			List<T> items;

			items = new List<T>();


			foreach(ShiftOnNonTerminal<T> action in State.NonTerminalActions.Where(item=>item.Name==Name))
			{
				nextState = GetTargetState(action.TargetStateIndex);
				foreach (ShiftOnTerminal<T> terminalAction in nextState.TerminalActions)
				{
					if (!items.Contains(terminalAction.Value)) items.Add(terminalAction.Value);
				}
			}
			

			return items;
		}

		public IEnumerable<T> GetFirstTerminalsForRule(IEnumerable<Rule<T>> Rules,string Name)
		{
			Segment<T> segment;
			List<T> items;

			items = new List<T>();

			if (openList.Contains(Name)) return items;
			openList.Add(Name);
			foreach (Rule<T> rule in Rules.Where(item => item.Name == Name))
			{
	
				segment = BuildSegment(rule, Enumerable.Empty<BaseAction<T>>());

				foreach (ShiftOnTerminal<T> action in segment.Actions.OfType<ShiftOnTerminal<T>>())
				{
					if (!items.Contains(action.Value)) items.Add(action.Value);
				}

				foreach (ShiftOnNonTerminal<T> action in segment.Actions.OfType<ShiftOnNonTerminal<T>>())
				{
					foreach(T input in GetFirstTerminalsForRule(Rules,action.Name))
					{
						if (!items.Contains(input)) items.Add(input);
					}
				}

			}
			openList.Remove(Name);

			return items;
		}

		/*public IEnumerable<Segment<T>> GetDeveloppedSegmentsForRule(IEnumerable<Rule<T>> Rules, string Name)
		{
			Segment<T> segment;

			if (openList.Contains(Name)) yield break;
			openList.Add(Name);

			foreach (Rule<T> rule in Rules.Where(item => item.Name == Name))
			{

				segment = BuildSegment(rule, Enumerable.Empty<BaseAction<T>>());

				yield return segment;
				
				foreach (NonTerminalAction<T> nonTerminalAction in segment.Inputs.OfType<NonTerminalAction<T>>())
				{
					foreach (Segment<T> nestedSegment in GetDeveloppedSegmentsForRule(Rules, nonTerminalAction.Name))
					{
						yield return nestedSegment;
					}
				}

			}
			openList.Remove(Name);


		}//*/

		public IEnumerable<string> GetRuleReductionDependency(IEnumerable<Rule<T>> Rules, string Name)
		{
			Segment<T> segment;
			List<string> items;

			items = new List<string>();

			if (openList.Contains(Name)) return items;
			openList.Add(Name);

			foreach (Rule<T> rule in Rules.Where(item => item.Name == Name))
			{

				segment = BuildSegment(rule, Enumerable.Empty<BaseAction<T>>());

				if (!items.Contains(rule.Name)) items.Add(rule.Name);

				foreach (ShiftOnNonTerminal<T> nonTerminalAction in segment.Actions.OfType<ShiftOnNonTerminal<T>>())
				{
					foreach (string dependantRule in GetRuleReductionDependency(Rules, nonTerminalAction.Name))
					{
						if (!items.Contains(dependantRule)) items.Add(dependantRule);
					}
				}

			}
			openList.Remove(Name);
			return items;
		}

		public IEnumerable<Reduce<T>> GetReductionActions(string Name)
		{
			return automatonTable.States.SelectMany(item => item.ReductionActions).Where(item=>item.Name==Name);
		}



	}
}
