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
		public void SouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new SegmentFactoryProvider<char>(null, new MockedNodeConnector()));
			Assert.ThrowsException<ArgumentNullException>(() => new SegmentFactoryProvider<char>(new MockedNodeContainer(), null));
		}
		[TestMethod]
		public void SouldProvideOneSequenceFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>(new MockedNodeContainer(), new MockedNodeConnector());
			factory = provider.GetSegmentFactory(new One<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(OneSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldProvideOrSequenceFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>(new MockedNodeContainer(), new MockedNodeConnector());
			factory = provider.GetSegmentFactory(new Or<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(OrSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldProvideSequenceSequenceFactory()
		{
			SegmentFactoryProvider<char> provider;
			ISegmentFactory<char> factory;

			provider = new SegmentFactoryProvider<char>(new MockedNodeContainer(), new MockedNodeConnector());
			factory = provider.GetSegmentFactory(new Sequence<char>());
			Assert.IsNotNull(factory);
			Assert.IsInstanceOfType(factory, typeof(SequenceSegmentFactory<char>));
		}
		[TestMethod]
		public void SouldFailOnInvalidPredicate()
		{
			SegmentFactoryProvider<char> provider;

			provider = new SegmentFactoryProvider<char>(new MockedNodeContainer(), new MockedNodeConnector());
			Assert.ThrowsException<NotImplementedException>(()=> provider.GetSegmentFactory(new MockedPredicate<char>()));
		}
		[TestMethod]
		public void SouldFailOnNullPredicate()
		{
			SegmentFactoryProvider<char> provider;

			provider = new SegmentFactoryProvider<char>(new MockedNodeContainer(), new MockedNodeConnector());
			Assert.ThrowsException<ArgumentNullException>(() => provider.GetSegmentFactory(null));
		}
	}
}
