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
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedAutomatonTableFactoryContext(), new MockedPredicate<char>(),Enumerable.Empty<Reduce<char>>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			OneOrMoreSegmentFactory<char> factory;

			factory = new OneOrMoreSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( null, new OneOrMore<char>(), Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedAutomatonTableFactoryContext(),  null, Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedAutomatonTableFactoryContext(),  new OneOrMore<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedSequencePredicate()
		{
			OneOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			AutomatonTable<char> automatonTable;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;
			OneOrMore<char> predicate;
			AutomatonTableFactoryContext<char> context;

			automatonTable = new AutomatonTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new AutomatonTableFactoryContext<char>(provider,automatonTable);

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
			Assert.AreEqual(3, automatonTable.States.Count);
			Assert.AreEqual(1, segment.Outputs.First().TerminalActions.Count);

			Assert.IsTrue(((ShiftOnTerminal<char>)segment.Actions.First()).Input.Match('a'));
			Assert.IsTrue(segment.Outputs.First().TerminalActions[0].Input.Match('a'));

		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedOrPredicate()
		{
			OneOrMoreSegmentFactory<char> factory;
			Segment<char> segment;
			AutomatonTable<char> automatonTable;
			SegmentFactoryProvider<char> provider;
			Or<char> or;
			OneOrMore<char> predicate;
			AutomatonTableFactoryContext<char> context;

			automatonTable = new AutomatonTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new AutomatonTableFactoryContext<char>(provider,automatonTable);
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
			Assert.AreEqual(3, automatonTable.States.Count);
			Assert.AreEqual(3, segment.Outputs.First().TerminalActions.Count);

			Assert.IsTrue( ((ShiftOnTerminal<char>)segment.Actions.First()).Input.Match('a'));
			Assert.IsTrue(segment.Outputs.First().TerminalActions[0].Input.Match('a'));
			Assert.IsTrue(segment.Outputs.First().TerminalActions[1].Input.Match('b'));
			Assert.IsTrue(segment.Outputs.First().TerminalActions[2].Input.Match('c'));
		
			


		}

	}
}
