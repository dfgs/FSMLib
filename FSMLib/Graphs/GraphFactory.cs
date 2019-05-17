using FSMLib.Graphs.Inputs;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class GraphFactory<T> : IGraphFactory<T>
	{
		private ISegmentFactoryProvider<T> segmentFactoryProvider;
		private ISituationProducer<T> situationProducer;

		public GraphFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider, ISituationProducer<T> SituationProducer)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (SituationProducer == null) throw new ArgumentNullException("SituationProducer");
			this.situationProducer = SituationProducer;
		}

		private void AddNonTerminalTransitionToNode(Node<T> node,IEnumerable<Transition<T>> Transitions, GraphFactoryContext<T> context, IEnumerable<Rule<T>> Rules)
		{
			Segment<T> nonTerminalSegment;
			
			foreach (NonTerminalInput<T> input in Transitions.Select(item => item.Input).OfType<NonTerminalInput<T>>().ToArray())
			{
				foreach (Rule<T> rule in Rules.Where(item => item.Name == input.Name))
				{
					nonTerminalSegment = context.BuildSegment(rule);
					node.RootIDs.Add(rule.GetHashCode());
					context.Connect(node.AsEnumerable(), nonTerminalSegment.Inputs);
					// we must connect recusively node to all segments, everytime a Non Terminal transition appears
					// but we must exclude Non Terminal transition with current input Name in order to avoid infinite loop
					AddNonTerminalTransitionToNode(node, nonTerminalSegment.Inputs.OfType<Transition<T>>().Where( item=> !(item.Input is NonTerminalInput<T> nextNonTerminalInput) || (nextNonTerminalInput.Name!=input.Name) ) , context, Rules);
				}
			}
		}

		public Graph<T> BuildGraph(IEnumerable<Rule<T>> Rules)
		{
			Graph<T> graph;
			Node<T> root;
			Segment<T> segment;
			GraphFactoryContext<T> context;
			
			
			if (Rules == null) throw new System.ArgumentNullException("Rules");

			graph = new Graph<T>();
			context = new GraphFactoryContext<T>(segmentFactoryProvider, graph);
			root = context.CreateNode();
			root.RootIDs.AddRange(Rules.Select(item=>item.GetHashCode()));
			// build all segments from rules
			foreach(Rule<T> rule in Rules)
			{
				segment = context.BuildSegment( rule);
				context.Connect( root.AsEnumerable(), segment.Inputs);
			}

			// check all transition in order to complete non terminal transitions
			foreach(Node<T> node in graph.Nodes)
			{
				if (node == root) continue;
				AddNonTerminalTransitionToNode(node,node.Transitions,context,Rules);
			}

			return graph;
		}


		public Graph<T> BuildDeterministicGraph(Graph<T> BaseGraph)
		{
			IEnumerable<Situation<T>> nextSituations;
			Graph<T> graph;
			List<GraphTuple<T>> situationMapping;
			GraphTuple<T> currentTuple,nextTuple;
			Stack<GraphTuple<T>> openList;
			Transition<T> transition;
			GraphFactoryContext<T> context;

			if (BaseGraph == null) throw new System.ArgumentNullException("BaseGraph");

			graph = new Graph<T>();
			if (BaseGraph.Nodes.Count == 0) return graph;
			context = new GraphFactoryContext<T>(segmentFactoryProvider,graph);

			situationMapping = new List<GraphTuple<T>>();
			openList = new Stack<GraphTuple<T>>();

			currentTuple = new GraphTuple<T>();
			currentTuple.Node = context.CreateNode();
			currentTuple.Situations = new Situation<T>() { Graph = BaseGraph, NodeIndex = 0 }.AsEnumerable();
			currentTuple.Node.RootIDs.AddRange(currentTuple.Situations.SelectMany(item => item.Graph.Nodes[item.NodeIndex].RootIDs));
			currentTuple.Node.MatchedRules.AddRange(currentTuple.Situations.SelectMany(item => item.Graph.Nodes[item.NodeIndex].MatchedRules));
			
			situationMapping.Add(currentTuple);
			openList.Push(currentTuple);

			while (openList.Count>0)
			{
				currentTuple = openList.Pop();
				foreach(IInput<T> input in situationProducer.GetNextInputs(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(currentTuple.Situations, input);
					// do we have the same situation list in graph ?
					nextTuple = situationMapping.FirstOrDefault(item => item.Situations.IsIndenticalToEx(nextSituations));
					if (nextTuple == null)
					{
						nextTuple = new GraphTuple<T>();
						nextTuple.Node = context.CreateNode();
						nextTuple.Situations = nextSituations;
						nextTuple.Node.RootIDs.AddRange(nextSituations.SelectMany(item => item.Graph.Nodes[item.NodeIndex].RootIDs));
						nextTuple.Node.MatchedRules.AddRange(nextSituations.SelectMany(item => item.Graph.Nodes[item.NodeIndex].MatchedRules));

						situationMapping.Add(nextTuple);
						openList.Push(nextTuple);
					}
					// if not we push this situation list in processing stack
					transition = new Transition<T>() { Input = input, TargetNodeIndex = context.GetNodeIndex(nextTuple.Node) };
					context.Connect( currentTuple.Node.AsEnumerable(),transition.AsEnumerable() );
				}
			}

			return graph;
		}









	}
	}
