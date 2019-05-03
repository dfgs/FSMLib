using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class OrSegmentFactory<T> : BaseSegmentFactory<Or<T>, T>
	{
		public OrSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base( SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment( INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, Or<T> Predicate, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			Segment<T> segment;
			Segment<T>[] segments;
			ISegmentFactory<T> childSegmentFactory;

			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");


			segments = new Segment<T>[Predicate.Items.Count];
			// create segments
			for (int t = 0; t < Predicate.Items.Count; t++)
			{
				childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Items[t]);
				segments[t] = childSegmentFactory.BuildSegment(NodeContainer,NodeConnector, Predicate.Items[t],OutTransitions);
			}
			

			segment = new Segment<T>();
			segment.Inputs = segments.SelectMany(item=>item.Inputs);
			segment.Outputs = segments.SelectMany(item => item.Outputs);

			return segment;
		}
	}
}
