using FSMLib.Graphs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SegmentFactories
{
	public class SegmentFactoryProvider<T> : ISegmentFactoryProvider<T>
	{
		private OneSegmentFactory<T> oneSegmentFactory;
		private SequenceSegmentFactory<T> sequenceSegmentFactory;
		private OrSegmentFactory<T> orSegmentFactory;

		public SegmentFactoryProvider(INodeContainer NodeContainer,INodeConnector NodeConnector)
		{
			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			oneSegmentFactory = new OneSegmentFactory<T>(NodeContainer, NodeConnector, this);
			sequenceSegmentFactory = new SequenceSegmentFactory<T>(NodeContainer, NodeConnector, this);
			orSegmentFactory = new OrSegmentFactory<T>(NodeContainer, NodeConnector, this);
		}

		public ISegmentFactory<T> GetSegmentFactory(RulePredicate<T> Predicate)
		{
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			switch (Predicate)
			{
				case One<T> one:return oneSegmentFactory;
				case Sequence<T> sequence:return sequenceSegmentFactory;
				case Or<T> or:return orSegmentFactory;
				default:
					throw new System.NotImplementedException($"Invalid predicate type {Predicate.GetType()}");
			}
		}

	}
}
