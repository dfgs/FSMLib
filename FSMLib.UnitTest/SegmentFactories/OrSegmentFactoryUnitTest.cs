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
	public class OrSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new OrSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			OrSegmentFactory<char> factory;

			factory = new OrSegmentFactory<char>( new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(() => factory.BuildSegment(new MockedAutomatonTableFactoryContext(),  new MockedPredicate<char>(), Enumerable.Empty<Reduce<char>>()));
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			OrSegmentFactory<char> factory;

			factory = new OrSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null,  new Or<char>(), Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedAutomatonTableFactoryContext(),  null, Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedAutomatonTableFactoryContext(),  new Or<char>(),null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			OrSegmentFactory<char> factory;
			Segment<char> segment;
			AutomatonTable<char> automatonTable;
			SegmentFactoryProvider<char> provider;
			Or<char> Or;
			AutomatonTableFactoryContext<char> context;

			automatonTable = new AutomatonTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new AutomatonTableFactoryContext<char>(provider,automatonTable);
			factory = new OrSegmentFactory<char>( provider);

			Or = new Or<char>();
			Or.Items.Add(new Terminal<char>() { Value = 'a' });
			Or.Items.Add(new Terminal<char>() { Value = 'b' });
			Or.Items.Add(new Terminal<char>() { Value = 'c' });

			segment = factory.BuildSegment(context,  Or, Enumerable.Empty<Reduce<char>>());
			Assert.IsNotNull(segment);
			Assert.AreEqual(3, segment.Actions.Count());
			Assert.AreEqual(3, segment.Outputs.Count());
			Assert.AreEqual(3, automatonTable.States.Count);
			Assert.AreEqual(true, ((ShiftOnTerminal<char>)segment.Actions.ElementAt(0)).Match('a'));
			Assert.AreEqual(true, ((ShiftOnTerminal<char>)segment.Actions.ElementAt(1)).Match('b'));
			Assert.AreEqual(true, ((ShiftOnTerminal<char>)segment.Actions.ElementAt(2)).Match('c'));
		}



	}
}
