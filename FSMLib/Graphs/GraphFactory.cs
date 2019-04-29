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
		private Func<INodeContainer,INodeConnector> connectorFunc;
		private Func<INodeContainer, INodeConnector,ISegmentFactoryProvider<T>> providerFunc;

		public GraphFactory(Func<INodeContainer, INodeConnector> ConnectorFunc, Func<INodeContainer, INodeConnector,ISegmentFactoryProvider<T>> ProviderFunc)
		{
			if (ProviderFunc == null) throw new ArgumentNullException("ProviderFunc");
			this.providerFunc = ProviderFunc;
			if (ConnectorFunc == null) throw new ArgumentNullException("ConnectorFunc");
			this.connectorFunc = ConnectorFunc;
		}


		public Graph BuildGraph(IEnumerable<Rule<T>> Rules)
		{
			Graph graph;
			Node root;
			Segment segment;
			ISegmentFactory<T> segmentFactory;
			INodeConnector connector;
			ISegmentFactoryProvider<T> provider;

			if (Rules == null) throw new System.ArgumentNullException("Rules");



			graph = new Graph();
			root = graph.CreateNode();

			connector = connectorFunc(graph);
			provider = providerFunc(graph,connector);

			foreach(Rule<T> rule in Rules)
			{
				segmentFactory = provider.GetSegmentFactory(rule.Predicate);
				segment = segmentFactory.BuildSegment(rule.Predicate);
				connector.Connect(new Node[] { root }, segment.Inputs);
			}

			return graph;
		}

		

		

		

		
	


	}
}
