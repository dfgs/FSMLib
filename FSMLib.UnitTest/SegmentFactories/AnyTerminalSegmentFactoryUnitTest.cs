using FSMLib.ActionTables;
using FSMLib.ActionTables.Actions;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
			Assert.ThrowsException<InvalidCastException>(()=> factory.BuildSegment( new MockedActionTableFactoryContext() , new MockedPredicate<char>(),Enumerable.Empty<Reduce<char>>() ));;
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			AnyTerminalSegmentFactory<char> factory;

			factory = new AnyTerminalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null,  new AnyTerminal<char>(), Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedActionTableFactoryContext(),  null, Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedActionTableFactoryContext(),  new AnyTerminal<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			AnyTerminalSegmentFactory<char> factory;
			Segment<char> segment;
			ActionTable<char> actionTable;
			SegmentFactoryProvider<char> provider;
			ActionTableFactoryContext<char> context;

			actionTable = new ActionTable<char>();
			actionTable.Alphabet.AddRange(new char[] { 'a', 'b', 'c' });
			provider = new SegmentFactoryProvider<char>();
			context = new ActionTableFactoryContext<char>(provider,actionTable);
			factory = new AnyTerminalSegmentFactory<char>( provider);

			segment=factory.BuildSegment( context,  new AnyTerminal<char>(), Enumerable.Empty<Reduce<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(3, segment.Actions.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, actionTable.States.Count);
			Assert.AreEqual(true, ((ShiftOnTerminal<char>)segment.Actions.First()).Match('a'));
		}



	}
}
