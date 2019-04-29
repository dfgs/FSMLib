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

		public SegmentFactoryProvider()
		{
			oneSegmentFactory = new OneSegmentFactory<T>(this);
			sequenceSegmentFactory = new SequenceSegmentFactory<T>(this);
			orSegmentFactory = new OrSegmentFactory<T>( this);
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
