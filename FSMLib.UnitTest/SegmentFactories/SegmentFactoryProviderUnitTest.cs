using System;
using FSMLib.Graphs;
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
		public void SouldProvideOneSequenceFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>();
			factory = provider.GetSegmentFactory(new One<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(OneSegmentFactory<char>));
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
		public void SouldProvideAnySegmentFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>();
			factory = provider.GetSegmentFactory(new Any<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(AnySegmentFactory<char>));
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
