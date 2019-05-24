using System;
using FSMLib.ActionTables;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.SegmentFactories
{
	[TestClass]
	public class SegmentFactoryProviderUnitTest
	{
		
		[TestMethod]
		public void SouldProvideTerminalSequenceFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>();
			factory = provider.GetSegmentFactory(new Terminal<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(TerminalSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldProvideNonTerminalSequenceFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>();
			factory = provider.GetSegmentFactory(new NonTerminal<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(NonTerminalSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldProvideOrSequenceFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>();
			factory = provider.GetSegmentFactory(new Or<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(OrSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldProvideSequenceSequenceFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>();
			factory = provider.GetSegmentFactory(new Sequence<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(SequenceSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldProvideAnyTerminalSegmentFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>();
			factory = provider.GetSegmentFactory(new AnyTerminal<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(AnyTerminalSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldProvideOneOrMoreSegmentFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>();
			factory = provider.GetSegmentFactory(new OneOrMore<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(OneOrMoreSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldProvideZeroOrMoreSegmentFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>();
			factory = provider.GetSegmentFactory(new ZeroOrMore<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(ZeroOrMoreSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldProvideOptionalSegmentFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>();
			factory = provider.GetSegmentFactory(new Optional<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(OptionalSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldFailOnInvalidPredicate()
		{
			SegmentFactoryProvider<char> provider;

			provider = new SegmentFactoryProvider<char>();
			Assert.ThrowsException<NotImplementedException>(()=> provider.GetSegmentFactory(new MockedPredicate<char>()));
		}
		[TestMethod]
		public void SouldFailOnNullPredicate()
		{
			SegmentFactoryProvider<char> provider;

			provider = new SegmentFactoryProvider<char>();
			Assert.ThrowsException<ArgumentNullException>(() => provider.GetSegmentFactory(null));
		}
	}
}
