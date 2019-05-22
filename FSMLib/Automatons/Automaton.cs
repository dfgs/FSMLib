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

		public void Feed(T Input)
		{
			TerminalNode<T> inputNode;
			NonTerminalNode<T> nonTerminalNode;

			inputNode = new TerminalNode<T>() { Value=Input };

		retry:

			if (Feed(inputNode)) return;
			
			if (CanReduce())
			{
				nonTerminalNode = Reduce();
				if (!Feed(nonTerminalNode)) throw new AutomatonException<T>(Input, nodeStack);
				goto retry;
			}

			throw new AutomatonException<T>(Input, nodeStack);
		}


		private bool CanReduce()
		{
			Node<T> node;

			node = graph.Nodes[nodeIndex];
			return node.ReductionTransitions.Count > 0;
		}

		private NonTerminalNode<T> Reduce()
		{
			Node<T> node;
			BaseNode<T> baseNode;
			ReductionTransition<T> reductionTransition;
			NonTerminalNode<T> reducedNode;


			node = graph.Nodes[nodeIndex];
			if (node.ReductionTransitions.Count == 0) throw new InvalidOperationException("Automaton cannot reduce in current state");

			reductionTransition = node.ReductionTransitions[0];
			reducedNode = new NonTerminalNode<T>() { Name= reductionTransition.Name };

			
			while (nodeStack.Count>0 &&  (nodeIndex!=reductionTransition.TargetNodeIndex) )
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
			AcceptTransition<T> axiom;

			node = graph.Nodes[nodeIndex];
			axiom = node.AcceptTransitions.FirstOrDefault();

			return axiom != null;
		}

		public NonTerminalNode<T> Accept()
		{
			if (!CanAccept()) throw new InvalidOperationException("Automaton cannot accept in current state");
			return Reduce();
		}


	}
}
