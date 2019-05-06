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

		public override Segment<T> BuildSegment( IGraphFactoryContext<T> Context, Sequence<T> Predicate, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			Segment<T> segment;
			Segment<T>[] segments;
			ISegmentFactory<T> childSegmentFactory;
			IEnumerable<BaseTransition<T>> nextTransitions;

			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			nextTransitions = OutTransitions;
			segments = new Segment<T>[Predicate.Items.Count];
			// create segments
			for (int t= Predicate.Items.Count-1; t>=0;t--)
			{
				childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Items[t]);
				segments[t]=childSegmentFactory.BuildSegment(Context, Predicate.Items[t],nextTransitions);
				nextTransitions = segments[t].Inputs;
			}
			
			segment = new Segment<T>();
			segment.Inputs = nextTransitions;
			segment.Outputs = segments.Last().Outputs;

			return segment;
		}


	}
}
