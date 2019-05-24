using FSMLib.Table;
using FSMLib.Actions;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FSMLib.UnitTest.SegmentFactories
{
	[TestClass]
	public class TerminalSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new TerminalSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			TerminalSegmentFactory<char> factory;

			factory = new TerminalSegmentFactory<char>( new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(()=> factory.BuildSegment(new MockedAutomatonTableFactoryContext() , new MockedPredicate<char>(), Enumerable.Empty<Reduce<char>>()));;
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			TerminalSegmentFactory<char> factory;

			factory = new TerminalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null,  new Terminal<char>(), Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedAutomatonTableFactoryContext(),  null, Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedAutomatonTableFactoryContext(),  new Terminal<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			TerminalSegmentFactory<char> factory;
			Segment<char> segment;
			AutomatonTable<char> automatonTable;
			SegmentFactoryProvider<char> provider;
			AutomatonTableFactoryContext<char> context;

			automatonTable = new AutomatonTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new AutomatonTableFactoryContext<char>(provider,automatonTable);
			factory = new TerminalSegmentFactory<char>( provider);

			segment=factory.BuildSegment(context,  new Terminal<char>() { Value='a' }, Enumerable.Empty<Reduce<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Actions.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, automatonTable.States.Count);
			Assert.AreEqual('a', ((ShiftOnTerminal<char>)segment.Actions.First()).Input.Value);
		}



	}
}
