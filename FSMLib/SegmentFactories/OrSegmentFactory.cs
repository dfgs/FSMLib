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
	public class OrSegmentFactory<T> : BaseSegmentFactory<Or<T>, T>
	{
		public OrSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base( SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment( IActionTableFactoryContext<T> Context, Or<T> Predicate, IEnumerable<BaseAction<T>> OutActions)
		{
			Segment<T> segment;
			Segment<T>[] segments;
			ISegmentFactory<T> childSegmentFactory;

			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutActions == null) throw new ArgumentNullException("OutActions");


			segments = new Segment<T>[Predicate.Items.Count];
			// create segments
			for (int t = 0; t < Predicate.Items.Count; t++)
			{
				childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Items[t]);
				segments[t] = childSegmentFactory.BuildSegment(Context, Predicate.Items[t],OutActions);
			}
			

			segment = new Segment<T>();
			segment.Actions = segments.SelectMany(item=>item.Actions);
			segment.Outputs = segments.SelectMany(item => item.Outputs);

			return segment;
		}
	}
}
