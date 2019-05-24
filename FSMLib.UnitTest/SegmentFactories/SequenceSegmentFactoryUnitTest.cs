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
	public class SequenceSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new SequenceSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			SequenceSegmentFactory<char> factory;

			factory = new SequenceSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedAutomatonTableFactoryContext(),new MockedPredicate<char>(), Enumerable.Empty<Reduce<char>>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			SequenceSegmentFactory<char> factory;

			factory = new SequenceSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( null,  new Sequence<char>(), Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedAutomatonTableFactoryContext(),  null, Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment( new MockedAutomatonTableFactoryContext(),  new Sequence<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			SequenceSegmentFactory<char> factory;
			Segment<char> segment;
			AutomatonTable<char> automatonTable;
			SegmentFactoryProvider<char> provider;
			Sequence<char> sequence;
			AutomatonTableFactoryContext<char> context;

			automatonTable = new AutomatonTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new AutomatonTableFactoryContext<char>(provider,automatonTable);
			factory = new SequenceSegmentFactory<char>( provider);

			sequence = new Sequence<char>();
			sequence.Items.Add(new Terminal<char>() { Value='a' } );
			sequence.Items.Add(new Terminal<char>() { Value = 'b' });
			sequence.Items.Add(new Terminal<char>() { Value = 'c' });

			segment = factory.BuildSegment(context, sequence, Enumerable.Empty<Reduce<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Actions.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(3, automatonTable.States.Count);

			Assert.AreEqual(true, ((ShiftOnTerminal<char>)segment.Actions.First()).Match('a'));

		}


	}
}
