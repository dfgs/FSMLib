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
		}

		public void Reset()
		{
			nodeIndex = 0;
			nodeStack.Clear();
			nodeIndexStack.Clear();
		}

		

		private bool Feed(BaseNode<T> Node,IInput<T> Input)
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			foreach (Transition<T> transition in node.Transitions.OrderBy(item => item.Input.Priority)) // must match input with lower priority first
			{
				if (transition.Input.Match(Input))
				{
					nodeIndexStack.Push(nodeIndex);
					nodeStack.Push(Node);
					nodeIndex = transition.TargetNodeIndex;
					return true;
				}
			}
			return false;
		}

		public void Feed(T Item)
		{
			TerminalNode<T> inputNode;
			TerminalInput<T> terminalInput;
			
			NonTerminalNode<T> nonTerminalNode;
			NonTerminalInput<T> nonTerminalInput;


			terminalInput = new TerminalInput<T>() { Value = Item };
			inputNode = new TerminalNode<T>() { Value=Item };

		retry:

			if (Feed(inputNode, terminalInput)) return;
			
			if (CanReduce())
			{
				nonTerminalNode = Reduce();
				nonTerminalInput = new NonTerminalInput<T>() { Name = nonTerminalNode.Name };
				if (!Feed(nonTerminalNode, nonTerminalInput)) throw new AutomatonException<T>(nonTerminalInput, nodeStack);
				goto retry;
			}

			throw new AutomatonException<T>(terminalInput, nodeStack);
		}


		private bool CanReduce()
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			return node.MatchedRules.Count > 0;
		}

		private NonTerminalNode<T> Reduce()
		{
			Node<T> node;
			BaseNode<T> baseNode;
			MatchedRule matchedRule;
			NonTerminalNode<T> reducedNode;


			node = graph.Nodes[nodeIndex];
			if (node.MatchedRules.Count == 0) throw new InvalidOperationException("Automaton cannot reduce in current state");

			matchedRule = node.MatchedRules[0];
			reducedNode = new NonTerminalNode<T>() { Name= matchedRule.Name };


			while (nodeStack.Count>0 &&  (!graph.Nodes[nodeIndex].RootIDs.Contains(matchedRule.ID)) )
			{
				nodeIndex = nodeIndexStack.Pop();
				baseNode=nodeStack.Pop();
				reducedNode.Nodes.Insert(0,baseNode);	// stack order is inverted compared to node childs
			}

			return reducedNode;
			
		}

		public bool CanAccept()
		{
			Node<T> node;
			MatchedRule axiom;

			node = graph.Nodes[nodeIndex];
			axiom = node.MatchedRules.FirstOrDefault(item => item.IsAxiom);

			return axiom != null;
		}

		public NonTerminalNode<T> Accept()
		{
			if (!CanAccept()) throw new InvalidOperationException("Automaton cannot accept in current state");
			return Reduce();
		}


	}
}
