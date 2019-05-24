using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Table;
using FSMLib.Actions;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class OptionalSegmentFactory<T> : BaseSegmentFactory<Optional<T>, T>
	{
		public OptionalSegmentFactory(ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment( IAutomatonTableFactoryContext<T> Context, Optional<T> Predicate, IEnumerable<BaseAction<T>> OutActions)
		{
			ISegmentFactory<T> childSegmentFactory;
			Segment<T> segment,newSegment;

			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutActions == null) throw new ArgumentNullException("OutActions");

			// create child segment
			childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Item);
			segment = childSegmentFactory.BuildSegment(Context,  Predicate.Item, OutActions);


			newSegment = new Segment<T>();
			newSegment.Outputs = segment.Outputs;
			newSegment.Actions = segment.Actions.Concat(OutActions);

			return newSegment;
		}


	}
}
