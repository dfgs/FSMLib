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

		public override Segment<T> BuildSegment( IGraphFactoryContext<T> Context,  OneOrMore<T> Predicate, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			ISegmentFactory<T> childSegmentFactory;
			Segment<T> segment;

			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			// create child segment
			childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Item);
			segment=childSegmentFactory.BuildSegment(Context, Predicate.Item,OutTransitions);
			
			// connect segments
			Context.Connect(segment.Outputs , segment.Inputs);
			
			
			return segment;
		}


	}
}
