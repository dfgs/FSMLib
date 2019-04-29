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
		private INodeConnector nodeConnector;
		private ISegmentFactoryProvider<T> segmentFactoryProvider;

		public GraphFactory( INodeConnector NodeConnector, ISegmentFactoryProvider<T> SegmentFactoryProvider)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			this.nodeConnector = NodeConnector;
		}


		public Graph BuildGraph(IEnumerable<Rule<T>> Rules)
		{
			Graph graph;
			Node root;
			Segment segment;
			ISegmentFactory<T> segmentFactory;

			if (Rules == null) throw new System.ArgumentNullException("Rules");



			graph = new Graph();
			root = graph.CreateNode();


			foreach(Rule<T> rule in Rules)
			{
				segmentFactory = segmentFactoryProvider.GetSegmentFactory(rule.Predicate);
				segment = segmentFactory.BuildSegment(graph,nodeConnector, rule.Predicate);
				nodeConnector.Connect(graph,new Node[] { root }, segment.Inputs);
			}

			return graph;
		}

		

		

		

		
	


	}
}
