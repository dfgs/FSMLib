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


		public abstract Segment<T> BuildSegment( IActionTableFactoryContext<T> Context, TPredicate Predicate, IEnumerable<BaseAction<T>> OutActions);
		

		public Segment<T> BuildSegment( IActionTableFactoryContext<T> Context, BasePredicate<T> Predicate, IEnumerable<BaseAction<T>> OutActions)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutActions == null) throw new ArgumentNullException("OutActions");

			if (Predicate is TPredicate predicate) return BuildSegment(Context, predicate,OutActions);
			else throw new InvalidCastException("Predicate type is not compatible with this segment factory");
		}

	}
}
