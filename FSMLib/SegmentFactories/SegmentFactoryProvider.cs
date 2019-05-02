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
		private ZeroOrMoreSegmentFactory<T> zeroOrMoreSegmentFactory;

		public SegmentFactoryProvider()
		{
			oneSegmentFactory = new OneSegmentFactory<T>(this);
			anySegmentFactory = new AnySegmentFactory<T>(this);
			sequenceSegmentFactory = new SequenceSegmentFactory<T>(this);
			orSegmentFactory = new OrSegmentFactory<T>( this);
			oneOrMoreSegmentFactory = new OneOrMoreSegmentFactory<T>(this);
			zeroOrMoreSegmentFactory = new ZeroOrMoreSegmentFactory<T>(this);
		}

		public ISegmentFactory<T> GetSegmentFactory(RulePredicate<T> Predicate)
		{
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			switch (Predicate)
			{
				case One<T> predicate: return oneSegmentFactory;
				case Any<T> predicate: return anySegmentFactory;
				case Sequence<T> predicate: return sequenceSegmentFactory;
				case Or<T> predicate: return orSegmentFactory;
				case OneOrMore<T> predicate: return oneOrMoreSegmentFactory;
				case ZeroOrMore<T> predicate: return zeroOrMoreSegmentFactory;
				default:
					throw new System.NotImplementedException($"Invalid predicate type {Predicate.GetType()}");
			}
		}

	}
}
