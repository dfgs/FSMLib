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
	public class OneOrMoreSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new OneOrMoreSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			OneOrMoreSegmentFactory<char> factory;

			factory = new OneOrMoreSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedActionTableFactoryContext(), new MockedPredicate<char>(),Enumerable.Empty<Reduce<char>>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			OneOrMoreSegmentFactory<char> factory;

			factory = new OneOrMoreSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( null, new OneOrMore<char>(), Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedActionTableFactoryContext(),  null, Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedActionTableFactoryContext(),  new OneOrMore<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedSequencePredicate()
		{
			OneOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			ActionTable<char> actionTable;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;
			OneOrMore<char> predicate;
			ActionTableFactoryContext<char> context;

			actionTable = new ActionTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new ActionTableFactoryContext<char>(provider,actionTable);

			factory = new OneOrMoreSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value='a' } );
			sequence.Items.Add(new Terminal<char>() { Value = 'b' });
			sequence.Items.Add(new Terminal<char>() { Value = 'c' });

			predicate = new OneOrMore<char>() {  Item=sequence};

			segment = factory.BuildSegment(context,  predicate,Enumerable.Empty<Reduce<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Actions.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, actionTable.States.Count);
			Assert.AreEqual(1, segment.Outputs.First().TerminalActions.Count);

			Assert.AreEqual(true, ((ShiftOnTerminal<char>)segment.Actions.First()).Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalActions[0].Match('a'));

		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedOrPredicate()
		{
			OneOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			ActionTable<char> actionTable;
			SegmentFactoryProvider<char> provider;
			Or<char> or;
			OneOrMore<char> predicate;
			ActionTableFactoryContext<char> context;

			actionTable = new ActionTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new ActionTableFactoryContext<char>(provider,actionTable);
			factory = new OneOrMoreSegmentFactory<char>(provider);

			or = new Or<char>();
			or.Items.Add(new Terminal<char>() { Value = 'a' });
			or.Items.Add(new Terminal<char>() { Value = 'b' });
			or.Items.Add(new Terminal<char>() { Value = 'c' });

			predicate = new OneOrMore<char>() { Item = or };

			segment = factory.BuildSegment( context, predicate, Enumerable.Empty<Reduce<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(3, segment.Actions.Count());
			Assert.AreEqual(3, segment.Outputs.Count());
			Assert.AreEqual(3, actionTable.States.Count);
			Assert.AreEqual(3, segment.Outputs.First().TerminalActions.Count);

			Assert.AreEqual(true, ((ShiftOnTerminal<char>)segment.Actions.First()).Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalActions[0].Match('a'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalActions[1].Match('b'));
			Assert.AreEqual(true, segment.Outputs.First().TerminalActions[2].Match('c'));

		}

	}
}
