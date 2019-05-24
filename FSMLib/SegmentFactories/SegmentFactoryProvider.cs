using FSMLib.Table;
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
		private TerminalSegmentFactory<T> terminalSegmentFactory;
		private NonTerminalSegmentFactory<T> nonTerminalSegmentFactory;
		private AnyTerminalSegmentFactory<T> anyTerminalSegmentFactory;
		private SequenceSegmentFactory<T> sequenceSegmentFactory;
		private OrSegmentFactory<T> orSegmentFactory;
		private OneOrMoreSegmentFactory<T> oneOrMoreSegmentFactory;
		private ZeroOrMoreSegmentFactory<T> zeroOrMoreSegmentFactory;
		private OptionalSegmentFactory<T> optionalSegmentFactory;

		public SegmentFactoryProvider()
		{
			terminalSegmentFactory = new TerminalSegmentFactory<T>(this);
			nonTerminalSegmentFactory = new NonTerminalSegmentFactory<T>(this);
			anyTerminalSegmentFactory = new AnyTerminalSegmentFactory<T>(this);
			sequenceSegmentFactory = new SequenceSegmentFactory<T>(this);
			orSegmentFactory = new OrSegmentFactory<T>( this);
			oneOrMoreSegmentFactory = new OneOrMoreSegmentFactory<T>(this);
			zeroOrMoreSegmentFactory = new ZeroOrMoreSegmentFactory<T>(this);
			optionalSegmentFactory = new OptionalSegmentFactory<T>(this);
		}

		public ISegmentFactory<T> GetSegmentFactory(BasePredicate<T> Predicate)
		{
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			switch (Predicate)
			{
				case Terminal<T> predicate: return terminalSegmentFactory;
				case NonTerminal<T> predicate: return nonTerminalSegmentFactory;
				case AnyTerminal<T> predicate: return anyTerminalSegmentFactory;
				case Sequence<T> predicate: return sequenceSegmentFactory;
				case Or<T> predicate: return orSegmentFactory;
				case OneOrMore<T> predicate: return oneOrMoreSegmentFactory;
				case ZeroOrMore<T> predicate: return zeroOrMoreSegmentFactory;
				case Optional<T> predicate: return optionalSegmentFactory;
				default:
					throw new System.NotImplementedException($"Invalid predicate type {Predicate.GetType()}");
			}
		}

	}
}
