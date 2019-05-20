using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public class Automaton<T>:IAutomaton<T>
	{
		private Graph<T> graph;
		private int nodeIndex;

		private Stack<BaseNode<T>> nodeStack;
		private Stack<int> nodeIndexStack;

		private int savedStackCount;
		private int savedNodeIndex;

		public int StackCount
		{
			get => nodeStack.Count;
		}

		public Automaton(Graph<T> Graph)
		{
			if (Graph == null) throw new ArgumentNullException("Graph");
			this.graph = Graph;
			nodeStack = new Stack<BaseNode<T>>();
			nodeIndexStack = new Stack<int>();
			nodeIndex = 0;
			savedStackCount = 0;
			savedNodeIndex = 0;
		}

		public void Reset()
		{
			nodeIndex = 0;
			savedStackCount = 0;
			savedNodeIndex = 0;
			nodeStack.Clear();
			nodeIndexStack.Clear();
		}

		public void SaveSituation()
		{
			savedStackCount = StackCount;
			savedNodeIndex = nodeIndex; ;
		}
		public void RestoreSituation()
		{
			while(StackCount>savedStackCount)
			{
				nodeStack.Pop();
				nodeIndexStack.Pop();
			}
			nodeIndex = savedNodeIndex;
		}



		public bool Feed(IInput<T> Item)
		{
			Node<T> node;
			BaseNode<T> baseNode;

			if (Item is TerminalInput<T> terminalInput) baseNode = new TerminalNode<T>() { Value = terminalInput.Value };
			else if (Item is NonTerminalInput<T> nonTerminalInput) baseNode = new NonTerminalNode<T>() { Name = nonTerminalInput.Name };
			else throw new ArgumentException("Invalid input provided");

			node = graph.Nodes[nodeIndex];
			foreach(Transition<T> transition in node.Transitions.OrderBy(item=>item.Input.Priority))	// must match input with lower priority first
			{
				if (transition.Input.Match(Item))
				{
					nodeIndexStack.Push(nodeIndex);
					nodeStack.Push(baseNode);
					nodeIndex = transition.TargetNodeIndex;
					return true;
				}
			}

			return false;
		}
		public bool Feed(T Item)
		{
			return Feed(new TerminalInput<T>() { Value=Item });
		}

		public bool CanReduce()
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			return node.MatchedRules.Count > 0;
		}

		public NonTerminalNode<T> Reduce()
		{
			Node<T> node;
			BaseNode<T> baseNode;
			MatchedRule matchedRule;
			NonTerminalNode<T> reducedNode;


			node = graph.Nodes[nodeIndex];
			if (node.MatchedRules.Count == 0) return null;

			matchedRule = node.MatchedRules[0];
			reducedNode = new NonTerminalNode<T>() { Name= matchedRule.Name };


			while (nodeStack.Count>0 &&  (!graph.Nodes[nodeIndex].RootIDs.Contains(matchedRule.ID)) )
			{
				nodeIndex = nodeIndexStack.Pop();
				baseNode=nodeStack.Pop();
				reducedNode.Nodes.Add(baseNode);
			}

			return reducedNode;
			
		}


	}
}
