using FSMLib.ActionTables.Actions;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.ActionTables
{
	public class ActionTableFactoryContext<T> : IActionTableFactoryContext<T>
	{
		private List<Rule<T>> compilingList;
		private List<string> openList;

		private ActionTable<T> actionTable;
		private ISegmentFactoryProvider<T> segmentFactoryProvider;
		private Dictionary<Rule<T>, Segment<T>> cache;


		public ActionTableFactoryContext(ISegmentFactoryProvider<T> SegmentFactoryProvider, ActionTable<T> ActionTable)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (ActionTable == null) throw new ArgumentNullException("ActionTable");
			this.actionTable = ActionTable;

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
		public void Connect(IEnumerable<Node<T>> Nodes, IEnumerable<BaseAction<T>> Actions)
		{

			if (Nodes == null) throw new ArgumentNullException("Nodes");
			if (Actions == null) throw new ArgumentNullException("Actions");

			foreach (Node<T> node in Nodes)
			{
				foreach (BaseAction<T> action in Actions)
				{
					switch (action)
					{
						case ShiftOnTerminal<T> tr:
							node.TerminalActions.Add(tr);
							break;
						case ShifOnNonTerminal<T> tr:
							node.NonTerminalActions.Add(tr);
							break;
						case Reduce<T> tr:
							node.ReductionActions.Add(tr);
							break;
						case Accept<T> tr:
							node.AcceptActions.Add(tr);
							break;

						default:
							throw (new NotImplementedException("Invalid action type"));
					}

				}
			}
		}


		public Node<T> GetTargetNode(int Index)
		{
			if ((Index < 0) || (Index >= actionTable.Nodes.Count)) throw (new IndexOutOfRangeException("Node index is out of range"));
			return actionTable.Nodes[Index];
		}

		public Node<T> CreateNode()
		{
			Node<T> node;
			node = new Node<T>();
			node.Name = actionTable.Nodes.Count.ToString();
			actionTable.Nodes.Add(node);
			return node;
		}

		public int GetNodeIndex(Node<T> Node)
		{
			return actionTable.Nodes.IndexOf(Node);
		}

		public IEnumerable<T> GetAlphabet()
		{
			return actionTable.Alphabet;
		}


		public IEnumerable<T> GetFirstTerminalsAfterAction(Node<T> Node, string Name)
		{
			Node<T> nextNode;
			List<T> items;

			items = new List<T>();


			foreach(ShifOnNonTerminal<T> action in Node.NonTerminalActions.Where(item=>item.Name==Name))
			{
				nextNode = GetTargetNode(action.TargetNodeIndex);
				foreach (ShiftOnTerminal<T> terminalAction in nextNode.TerminalActions)
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

				foreach (ShifOnNonTerminal<T> action in segment.Actions.OfType<ShifOnNonTerminal<T>>())
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

				foreach (ShifOnNonTerminal<T> nonTerminalAction in segment.Actions.OfType<ShifOnNonTerminal<T>>())
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
			return actionTable.Nodes.SelectMany(item => item.ReductionActions).Where(item=>item.Name==Name);
		}



	}
}
