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

		private INodeConnector nodeConnector;

		public GraphFactory(INodeConnector NodeConnector, ISegmentFactoryProvider<T> SegmentFactoryProvider)
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
			ISegmentFactory<T> childSegmentFactory;

			if (Rules == null) throw new System.ArgumentNullException("Rules");

			graph = new Graph();
			root = graph.CreateNode();

			foreach(Rule<T> rule in Rules)
			{
				childSegmentFactory = segmentFactoryProvider.GetSegmentFactory(rule.Predicate);
				segment = childSegmentFactory.BuildSegment(rule.Predicate);
				nodeConnector.Connect(new Node[] { root }, segment.Inputs);
			}

			return graph;
		}

		

		

		

		
	


	}
}
