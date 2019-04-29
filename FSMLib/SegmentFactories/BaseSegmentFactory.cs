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
		where TPredicate:RulePredicate<T>
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


		public abstract Segment BuildSegment(INodeContainer NodeContainer, INodeConnector NodeConnector, TPredicate Predicate);
		

		public  Segment BuildSegment(INodeContainer NodeContainer, INodeConnector NodeConnector,RulePredicate<T> Predicate)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (Predicate == null) throw new ArgumentNullException("Predicate");

			if (Predicate is TPredicate predicate) return BuildSegment(NodeContainer,NodeConnector, predicate);
			else throw new InvalidCastException("Predicate type is not compatible with this segment factory");
		}

	}
}
