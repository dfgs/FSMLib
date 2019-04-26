using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class OneSegmentFactory<T> : BaseSegmentFactory<One<T>, T>
	{
		public OneSegmentFactory(INodeContainer NodeContainer, INodeConnector NodeConnector, ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(NodeContainer, NodeConnector, SegmentFactoryProvider)
		{
		}

		public override Segment BuildSegment(One<T> Predicate)
		{
			if (Predicate == null) throw new ArgumentNullException("Predicate");

			Segment segment;
			Node node;

			segment = new Segment();

			
			node = NodeContainer.CreateNode();
			segment.Inputs = new Node[] { node };
			segment.Outputs= new Node[] { node };

			return segment;
		}


	}
}
