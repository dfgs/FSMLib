using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class OptionalSegmentFactory<T> : BaseSegmentFactory<Optional<T>, T>
	{
		public OptionalSegmentFactory(ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment( IGraphFactoryContext<T> Context, Optional<T> Predicate, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			ISegmentFactory<T> childSegmentFactory;
			Segment<T> segment,newSegment;

			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			// create child segment
			childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Item);
			segment = childSegmentFactory.BuildSegment(Context,  Predicate.Item, OutTransitions);


			newSegment = new Segment<T>();
			newSegment.Outputs = segment.Outputs;
			newSegment.Inputs = segment.Inputs.Concat(OutTransitions);

			return newSegment;
		}


	}
}
