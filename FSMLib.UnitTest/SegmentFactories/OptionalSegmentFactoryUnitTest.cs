using FSMLib.Table;
using FSMLib.Table.Actions;
using FSMLib.Predicates;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FSMLib.UnitTest.SegmentFactories
{
	[TestClass]
	public class OptionalSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new OptionalSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			OptionalSegmentFactory<char> factory;

			factory = new OptionalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedAutomatonTableFactoryContext(),new MockedPredicate<char>(), Enumerable.Empty<Reduce<char>>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			OptionalSegmentFactory<char> factory;

			factory = new OptionalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( null,  new Optional<char>(), Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedAutomatonTableFactoryContext(), null, Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedAutomatonTableFactoryContext(),  new Optional<char>(),null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedSequencePredicate()
		{
			OptionalSegmentFactory<char> factory;
			Segment<char> segment;
			AutomatonTable<char> automatonTable;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;
			Optional<char> predicate;
			AutomatonTableFactoryContext<char> context;

			automatonTable = new AutomatonTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new AutomatonTableFactoryContext<char>(provider,automatonTable);

			factory = new OptionalSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value='a' } );
			sequence.Items.Add(new Terminal<char>() { Value = 'b' });
			sequence.Items.Add(new Terminal<char>() { Value = 'c' });

			predicate = new Optional<char>() {  Item=sequence};

			segment = factory.BuildSegment(context,  predicate, new ShiftOnTerminal<char>() { Value = 'd' }.AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(2, segment.Actions.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, automatonTable.States.Count);
			Assert.AreEqual(1, segment.Outputs.First().TerminalActions.Count);

			Assert.AreEqual(true, ((ShiftOnTerminal<char>)segment.Actions.ElementAt(0)).Match('a'));
			Assert.AreEqual(true, ((ShiftOnTerminal<char>)segment.Actions.ElementAt(1)).Match('d'));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromNestedOrPredicate()
		{
			OptionalSegmentFactory<char> factory;
			Segment<char> segment;
			AutomatonTable<char> automatonTable;
			SegmentFactoryProvider<char> provider;
			Or<char> or;
			Optional<char> predicate;
			AutomatonTableFactoryContext<char> context;

			automatonTable = new AutomatonTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new AutomatonTableFactoryContext<char>(provider,automatonTable);
			factory = new OptionalSegmentFactory<char>(provider);

			or = new Or<char>();
			or.Items.Add(new Terminal<char>() { Value = 'a' });
			or.Items.Add(new Terminal<char>() { Value = 'b' });
			or.Items.Add(new Terminal<char>() { Value = 'c' });

			predicate = new Optional<char>() { Item = or };

			segment = factory.BuildSegment(context,  predicate, new ShiftOnTerminal<char>() { Value = 'd' }.AsEnumerable());
			Assert.IsNotNull(segment);
			Assert.AreEqual(4, segment.Actions.Count());
			Assert.AreEqual(3, segment.Outputs.Count());
			Assert.AreEqual(3, automatonTable.States.Count);
			Assert.AreEqual(1, segment.Outputs.First().TerminalActions.Count);

			Assert.AreEqual(true, ((ShiftOnTerminal<char>)segment.Actions.First()).Match('a'));
			

		}

	}
}
