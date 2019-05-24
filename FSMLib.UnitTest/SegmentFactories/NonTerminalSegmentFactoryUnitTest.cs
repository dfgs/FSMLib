using FSMLib.Table;
using FSMLib.Table.Actions;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using FSMLib.UnitTest.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FSMLib.UnitTest.SegmentFactories
{
	[TestClass]
	public class NonTerminalSegmentFactoryUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new NonTerminalSegmentFactory<char>(null));
		}
		[TestMethod]
		public void ShouldFailWithInvalidPredicate()
		{
			NonTerminalSegmentFactory<char> factory;

			factory = new NonTerminalSegmentFactory<char>( new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<InvalidCastException>(()=> factory.BuildSegment(new MockedAutomatonTableFactoryContext() ,new MockedPredicate<char>(), Enumerable.Empty<Reduce<char>>() ));;
		}
		[TestMethod]
		public void ShouldFailWithNullParameters()
		{
			NonTerminalSegmentFactory<char> factory;

			factory = new NonTerminalSegmentFactory<char>(new MockedSegmentFactoryProvider<char>());
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(null, new NonTerminal<char>(), Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedAutomatonTableFactoryContext(), null, Enumerable.Empty<Reduce<char>>()));
			Assert.ThrowsException<ArgumentNullException>(() => factory.BuildSegment(new MockedAutomatonTableFactoryContext(),  new NonTerminal<char>(), null));
		}
		[TestMethod]
		public void ShouldBuildSegmentFromPredicate()
		{
			NonTerminalSegmentFactory<char> factory;
			Segment<char> segment;
			AutomatonTable<char> automatonTable;
			SegmentFactoryProvider<char> provider;
			AutomatonTableFactoryContext<char> context;
			Rule<char> rule;

			rule = new Rule<char>() { Name = "S", Predicate = new Terminal<char>() { Value = 'a' } };
			automatonTable = new AutomatonTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new AutomatonTableFactoryContext<char>(provider, automatonTable) ;
			factory = new NonTerminalSegmentFactory<char>(provider);

			segment = factory.BuildSegment(context,  new NonTerminal<char>() { Name = "S" }, Enumerable.Empty<Reduce<char>>());

			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Actions.Count());	// not two, because translation from non terminal input is done at automatonTable factory level
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, automatonTable.States.Count);
			//Assert.AreEqual(true, ((Action<char>)segment.Inputs.ElementAt(1)).Input.Match('a'));
			Assert.AreEqual(true, ((ShiftOnNonTerminal<char>)segment.Actions.ElementAt(0)).Match("S"));
		}

		[TestMethod]
		public void ShouldNotFailIfNonTerminalRuleDoesntExistsInContext()
		{
			NonTerminalSegmentFactory<char> factory;
			Segment<char> segment;
			AutomatonTable<char> automatonTable;
			SegmentFactoryProvider<char> provider;
			AutomatonTableFactoryContext<char> context;
			Rule<char> rule;

			rule = new Rule<char>() { Name = "S", Predicate = new Terminal<char>() { Value = 'a' } };
			automatonTable = new AutomatonTable<char>();
			provider = new SegmentFactoryProvider<char>();
			context = new AutomatonTableFactoryContext<char>(provider, automatonTable);
			factory = new NonTerminalSegmentFactory<char>(provider);

			segment = factory.BuildSegment(context, new NonTerminal<char>() { Name = "A" }, Enumerable.Empty<Reduce<char>>());

			Assert.IsNotNull(segment);
			Assert.AreEqual(1, segment.Actions.Count());
			Assert.AreEqual(1, segment.Outputs.Count());
			Assert.AreEqual(1, automatonTable.States.Count);
			Assert.AreEqual(true, ((ShiftOnNonTerminal<char>)segment.Actions.ElementAt(0)).Match( "A" ));
		}

	}
}
