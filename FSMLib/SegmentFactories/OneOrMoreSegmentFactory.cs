using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class OneOrMoreSegmentFactory<T> : BaseSegmentFactory<OneOrMore<T>, T>
	{
		public OneOrMoreSegmentFactory(ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment(INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, OneOrMore<T> Predicate, IEnumerable<Transition<T>> OutTransitions)
		{
			ISegmentFactory<T> childSegmentFactory;
			Segment<T> segment;

			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			// create child segment
			childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Item);
			segment=childSegmentFactory.BuildSegment(NodeContainer,NodeConnector, Predicate.Item,OutTransitions);
			
			// connect segments
			NodeConnector.Connect(segment.Outputs , segment.Inputs);
			
			
			return segment;
		}


	}
}
