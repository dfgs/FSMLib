using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class SequenceSegmentFactory<T> : BaseSegmentFactory<Sequence<T>, T>
	{
		public SequenceSegmentFactory(ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment(INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, Sequence<T> Predicate, IEnumerable<Transition<T>> OutTransitions)
		{
			Segment<T> segment;
			Segment<T>[] segments;
			ISegmentFactory<T> childSegmentFactory;
			IEnumerable<Transition<T>> nextTransitions;

			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			nextTransitions = OutTransitions;
			segments = new Segment<T>[Predicate.Items.Count];
			// create segments
			for (int t= Predicate.Items.Count-1; t>=0;t--)
			{
				childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Items[t]);
				segments[t]=childSegmentFactory.BuildSegment(NodeContainer,NodeConnector, Predicate.Items[t],nextTransitions);
				nextTransitions = segments[t].Inputs;
			}
			
			segment = new Segment<T>();
			segment.Inputs = nextTransitions;
			segment.Outputs = segments.Last().Outputs;

			return segment;
		}


	}
}
