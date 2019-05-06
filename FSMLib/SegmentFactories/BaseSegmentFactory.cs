using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
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


		public abstract Segment<T> BuildSegment( IGraphFactoryContext<T> NodeContainer, INodeConnector<T> NodeConnector, TPredicate Predicate, IEnumerable<BaseTransition<T>> OutTransitions);
		

		public Segment<T> BuildSegment( IGraphFactoryContext<T> NodeContainer, INodeConnector<T> NodeConnector,BasePredicate<T> Predicate, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			if (Predicate is TPredicate predicate) return BuildSegment(NodeContainer,NodeConnector, predicate,OutTransitions);
			else throw new InvalidCastException("Predicate type is not compatible with this segment factory");
		}

	}
}
