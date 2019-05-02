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
		private INodeConnector<T> nodeConnector;
		private ISegmentFactoryProvider<T> segmentFactoryProvider;
		private ISituationProducer<T> situationProducer;

		public GraphFactory( INodeConnector<T> NodeConnector, ISegmentFactoryProvider<T> SegmentFactoryProvider, ISituationProducer<T> SituationProducer)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			this.nodeConnector = NodeConnector;
			if (SituationProducer == null) throw new ArgumentNullException("SituationProducer");
			this.situationProducer = SituationProducer;
		}


		public Graph<T> BuildGraph(IEnumerable<Rule<T>> Rules)
		{
			Graph<T> graph;
			Node<T> root;
			Segment<T> segment;
			ISegmentFactory<T> segmentFactory;

			if (Rules == null) throw new System.ArgumentNullException("Rules");



			graph = new Graph<T>();
			root = graph.CreateNode();


			foreach(Rule<T> rule in Rules)
			{
				segmentFactory = segmentFactoryProvider.GetSegmentFactory(rule.Predicate);
				segment = segmentFactory.BuildSegment(graph,nodeConnector, rule.Predicate);
				nodeConnector.Connect( root.AsEnumerable(), segment.Inputs);
			}

			return graph;
		}


		public Graph<T> BuildDeterministicGraph(Graph<T> BaseGraph)
		{
			IEnumerable<Situation<T>> currentSituations,nextSituations;
			Graph<T> graph;
			List<GraphTuple<T>> situationMapping;
			Node<T> currentNode,nextNode;
			GraphTuple<T> currentTuple,nextTuple;
			Stack<GraphTuple<T>> openList;
			Transition<T> transition;

			if (BaseGraph == null) throw new System.ArgumentNullException("BaseGraph");

			graph = new Graph<T>();
			if (BaseGraph.Nodes.Count == 0) return graph;

			situationMapping = new List<GraphTuple<T>>();

			currentNode=graph.CreateNode();
			currentSituations = new Situation<T>() { Graph = BaseGraph, NodeIndex = 0 }.AsEnumerable();
			currentTuple = new GraphTuple<T>(currentNode, currentSituations);
			situationMapping.Add(currentTuple);

			openList = new Stack<GraphTuple<T>>();
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
						nextNode = graph.CreateNode();
						nextTuple = new GraphTuple<T>(nextNode, nextSituations);
						situationMapping.Add(nextTuple);
						openList.Push(nextTuple);
					}
					// if not we push this situation list in processing stack
					transition = new Transition<T>() { Input = input, TargetNodeIndex = graph.GetNodeIndex(nextTuple.Node) };
					nodeConnector.Connect( currentTuple.Node.AsEnumerable(),transition.AsEnumerable() );
				}
			}

			
			

			return graph;
		}









	}
	}
