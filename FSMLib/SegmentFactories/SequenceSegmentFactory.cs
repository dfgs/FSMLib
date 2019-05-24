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
	public class SequenceSegmentFactory<T> : BaseSegmentFactory<Sequence<T>, T>
	{
		public SequenceSegmentFactory(ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment( IAutomatonTableFactoryContext<T> Context, Sequence<T> Predicate, IEnumerable<BaseAction<T>> OutActions)
		{
			Segment<T> segment;
			Segment<T>[] segments;
			ISegmentFactory<T> childSegmentFactory;
			IEnumerable<BaseAction<T>> nextActions;

			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutActions == null) throw new ArgumentNullException("OutActions");

			nextActions = OutActions;
			segments = new Segment<T>[Predicate.Items.Count];
			// create segments
			for (int t= Predicate.Items.Count-1; t>=0;t--)
			{
				childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Items[t]);
				segments[t]=childSegmentFactory.BuildSegment(Context, Predicate.Items[t],nextActions);
				nextActions = segments[t].Actions;
			}
			
			segment = new Segment<T>();
			segment.Actions = nextActions;
			segment.Outputs = segments.Last().Outputs;

			return segment;
		}


	}
}
