using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.ActionTables;
using FSMLib.ActionTables.Actions;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class ZeroOrMoreSegmentFactory<T> : BaseSegmentFactory<ZeroOrMore<T>, T>
	{
		public ZeroOrMoreSegmentFactory(ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment( IActionTableFactoryContext<T> Context, ZeroOrMore<T> Predicate, IEnumerable<BaseAction<T>> OutActions)
		{
			ISegmentFactory<T> childSegmentFactory;
			Segment<T> segment,newSegment;

			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutActions == null) throw new ArgumentNullException("OutActions");

			// create child segment
			childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Item);
			segment = childSegmentFactory.BuildSegment(Context, Predicate.Item, OutActions);

			// connect segments
			Context.Connect(segment.Outputs, segment.Actions);

			newSegment = new Segment<T>();
			newSegment.Outputs = segment.Outputs;
			newSegment.Actions = segment.Actions.Concat(OutActions);

			return newSegment;
		}


	}
}
