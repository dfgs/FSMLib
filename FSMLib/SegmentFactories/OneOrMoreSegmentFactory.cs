using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Table;
using FSMLib.Table.Actions;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class OneOrMoreSegmentFactory<T> : BaseSegmentFactory<OneOrMore<T>, T>
	{
		public OneOrMoreSegmentFactory(ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment( IAutomatonTableFactoryContext<T> Context,  OneOrMore<T> Predicate, IEnumerable<BaseAction<T>> OutActions)
		{
			ISegmentFactory<T> childSegmentFactory;
			Segment<T> segment;

			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutActions == null) throw new ArgumentNullException("OutActions");

			// create child segment
			childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Item);
			segment=childSegmentFactory.BuildSegment(Context, Predicate.Item,OutActions);
			
			// connect segments
			Context.Connect(segment.Outputs , segment.Actions);
			
			
			return segment;
		}


	}
}
