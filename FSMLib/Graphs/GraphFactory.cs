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

		public GraphFactory( INodeConnector<T> NodeConnector, ISegmentFactoryProvider<T> SegmentFactoryProvider)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			this.nodeConnector = NodeConnector;
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
				nodeConnector.Connect(new Node<T>[] { root }, segment.Inputs);
			}

			return graph;
		}


		public Graph<T> BuildDeterministicGraph(Graph<T> BaseGraph,ISituationProducer<T> SituationProducer)
		{
			Situation<T> currentSituation;
			Graph<T> graph;

			if (BaseGraph == null) throw new System.ArgumentNullException("BaseGraph");
			if (SituationProducer == null) throw new System.ArgumentNullException("SituationProducer");

			graph = new Graph<T>();
			if (BaseGraph.Nodes.Count == 0) return graph;

			currentSituation = new Situation<T>() { Graph = BaseGraph, NodeIndex = 0 };

			/*foreach (Transition<T> transition in currentSituation.Graph.Nodes[currentSituation.NodeIndex].Transitions)
			{
				transition.Input
			}*/
			

			return null;
		}









	}
	}
