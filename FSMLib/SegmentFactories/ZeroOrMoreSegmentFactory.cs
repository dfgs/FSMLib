using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class ZeroOrMoreSegmentFactory<T> : BaseSegmentFactory<ZeroOrMore<T>, T>
	{
		public ZeroOrMoreSegmentFactory(ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment(INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, ZeroOrMore<T> Predicate)
		{
			Segment<T> segment;
			ISegmentFactory<T> childSegmentFactory;

			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			if (Predicate == null) throw new ArgumentNullException("Predicate");

			// create child segment
			childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Item);
			segment=childSegmentFactory.BuildSegment(NodeContainer,NodeConnector, Predicate.Item);
			
			// connect segments
			NodeConnector.Connect(segment.Outputs , segment.Inputs);
			throw new NotImplementedException();
			
			return segment;
		}


	}
}
