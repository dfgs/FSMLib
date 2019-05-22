using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public abstract class BaseSegmentFactory<TPredicate, T> : ISegmentFactory<TPredicate, T>
		where TPredicate:BasePredicate<T>
	{
		protected ISegmentFactoryProvider<T> SegmentFactoryProvider
		{
			get;
			private set;
		}
		

		public BaseSegmentFactory(ISegmentFactoryProvider<T> SegmentFactoryProvider)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.SegmentFactoryProvider = SegmentFactoryProvider;
		}


		public abstract Segment<T> BuildSegment( IGraphFactoryContext<T> Context, TPredicate Predicate, IEnumerable<BaseTransition<T>> OutTransitions);
		

		public Segment<T> BuildSegment( IGraphFactoryContext<T> Context, BasePredicate<T> Predicate, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			if (Predicate is TPredicate predicate) return BuildSegment(Context, predicate,OutTransitions);
			else throw new InvalidCastException("Predicate type is not compatible with this segment factory");
		}

	}
}
