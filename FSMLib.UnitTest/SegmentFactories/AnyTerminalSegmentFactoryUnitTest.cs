using FSMLib.Table;
using FSMLib.Actions;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using FSMLib.Inputs;

namespace FSMLib.UnitTest.SegmentFactories
{
	[TestClass]
	public class AnyTerminalSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new AnyTerminalSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			AnyTerminalSegmentFactory<char> factory;

			factory = new AnyTerminalSegmentFactory<char>( new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(()=> factory.BuildSegment( new MockedAutomatonTableFactoryContext() , new MockedPredicate<char>(),Enumerable.Empty<Reduce<char>>() ));;
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			AnyTerminalSegmentFactory<char> factory;

			factory = new AnyTerminalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null,  new AnyTerminal<char>(), Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedAutomatonTableFactoryContext(),  null, Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedAutomatonTableFactoryContext(),  new AnyTerminal<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			AnyTerminalSegmentFactory<char> factory;
			Segment<char> segment;
			AutomatonTable<char> automatonTable;
			SegmentFactoryProvider<char> provider;
			AutomatonTableFactoryContext<char> context;

			automatonTable = new AutomatonTable<char>();
			automatonTable.Alphabet.AddRange(new char[] { 'a', 'b', 'c' });
			provider = new SegmentFactoryProvider<char>();
			context = new AutomatonTableFactoryContext<char>(provider,automatonTable);
			factory = new AnyTerminalSegmentFactory<char>( provider);

			segment=factory.BuildSegment( context,  new AnyTerminal<char>(), Enumerable.Empty<Reduce<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(3, segment.Actions.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, automatonTable.States.Count);
			Assert.IsTrue((((ShiftOnTerminal<char>)segment.Actions.First())).Input.Match('a'));
		}



	}
}
