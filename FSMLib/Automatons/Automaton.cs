using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;
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

		

		private bool Feed(TerminalNode<T> Node)
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			foreach (TerminalTransition<T> transition in node.TerminalTransitions)
			{
				if (transition.Match(Node.Value))
				{
					nodeIndexStack.Push(nodeIndex);
					nodeStack.Push(Node);
					nodeIndex = transition.TargetNodeIndex;
					return true;
				}
			}
			return false;
		}

		private bool Feed(NonTerminalNode<T> Node)
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			foreach (NonTerminalTransition<T> transition in node.NonTerminalTransitions)
			{
				if (transition.Match(Node.Name))
				{
					nodeIndexStack.Push(nodeIndex);
					nodeStack.Push(Node);
					nodeIndex = transition.TargetNodeIndex;
					return true;
				}
			}
			return false;
		}

		private NonTerminalNode<T> Reduce(string Name,int TargetNodeIndex)
		{
			BaseNode<T> baseNode;
			NonTerminalNode<T> reducedNode;

			reducedNode = new NonTerminalNode<T>() { Name = Name };

			while (nodeStack.Count > 0 && (nodeIndex != TargetNodeIndex))
			{
				nodeIndex = nodeIndexStack.Pop();
				baseNode = nodeStack.Pop();
				reducedNode.Nodes.Insert(0, baseNode);  // stack order is inverted compared to node childs
			}

			return reducedNode;
		}
		private NonTerminalNode<T> Reduce(TerminalNode<T> Node)
		{
			Node<T> node;
			ReductionTarget<T> target;

			node = graph.Nodes[nodeIndex];

			foreach (ReductionTransition<T> transition in node.ReductionTransitions)
			{
				target = transition.Targets.FirstOrDefault(item =>  Node.Value.Equals(item.Value));
				if (target == null) continue;

				return Reduce(transition.Name, target.TargetNodeIndex);
			}
			return null;
		}

		public void Feed(T Input)
		{
			TerminalNode<T> inputNode;
			NonTerminalNode<T> nonTerminalNode;

			inputNode = new TerminalNode<T>() { Value=Input };

			while (true)
			{
				if (Feed(inputNode)) return;

				nonTerminalNode = Reduce(inputNode);
				if ((nonTerminalNode == null) || (!Feed(nonTerminalNode))) throw new AutomatonException<T>(Input, nodeStack);
			}		

		}


		/*private bool CanReduce()
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			return node.ReductionTransitions.Count > 0;
		}*/



		public bool CanAccept()
		{
			Node<T> node;
			AcceptTransition<T> axiom;

			node = graph.Nodes[nodeIndex];
			axiom = node.AcceptTransitions.FirstOrDefault();

			return axiom != null;
		}

		public NonTerminalNode<T> Accept()
		{
			Node<T> node;
			AcceptTransition<T> axiom;

			node = graph.Nodes[nodeIndex];
			axiom = node.AcceptTransitions.FirstOrDefault();

			if (axiom==null) throw new InvalidOperationException("Automaton cannot accept in current state");
			return Reduce( axiom.Name,0 );
		}


	}
}
