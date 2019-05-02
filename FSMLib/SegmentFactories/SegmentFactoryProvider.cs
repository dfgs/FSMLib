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
		private AnySegmentFactory<T> anySegmentFactory;
		private SequenceSegmentFactory<T> sequenceSegmentFactory;
		private OrSegmentFactory<T> orSegmentFactory;
		private OneOrMoreSegmentFactory<T> oneOrMoreSegmentFactory;

		public SegmentFactoryProvider()
		{
			oneSegmentFactory = new OneSegmentFactory<T>(this);
			anySegmentFactory = new AnySegmentFactory<T>(this);
			sequenceSegmentFactory = new SequenceSegmentFactory<T>(this);
			orSegmentFactory = new OrSegmentFactory<T>( this);
			oneOrMoreSegmentFactory = new OneOrMoreSegmentFactory<T>(this);
		}

		public ISegmentFactory<T> GetSegmentFactory(RulePredicate<T> Predicate)
		{
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			switch (Predicate)
			{
				case One<T> one: return oneSegmentFactory;
				case Any<T> any: return anySegmentFactory;
				case Sequence<T> sequence:return sequenceSegmentFactory;
				case Or<T> or: return orSegmentFactory;
				case OneOrMore<T> or: return oneOrMoreSegmentFactory;
				default:
					throw new System.NotImplementedException($"Invalid predicate type {Predicate.GetType()}");
			}
		}

	}
}
