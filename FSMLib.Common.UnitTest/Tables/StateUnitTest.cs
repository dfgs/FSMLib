using System;
using System.Linq;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Actions;
using FSMLib.Common.Table;
using FSMLib.Common.Actions;
using FSMLib.Common.UnitTest.Mocks;

namespace FSMLib.Common.UnitTest.AutomatonTables
{
	[TestClass]
	public class StateUnitTest
	{
		[TestMethod]
		public void ShoudAddShift()
		{
			State<char> state;
			Shift<char> action;

			action = new Shift<char>(new MockedTerminalInput('a'),1);
			state = new State<char>();
			state.Add(action);
			Assert.AreEqual(1, state.ShiftActionCount);
			Assert.IsTrue(state.ShiftActions.Contains(action));
		}

		[TestMethod]
		public void ShoudAddReduce()
		{
			State<char> state;
			Reduce<char> action;

			action = new Reduce<char>("A",true, new MockedTerminalInput('a'));
			state = new State<char>();
			state.Add(action);
			Assert.AreEqual(1, state.ReduceActionCount);
			Assert.IsTrue(state.ReduceActions.Contains(action));
		}

		[TestMethod]
		public void ShoudGetShiftUsingTerminalInput()
		{
			State<char> state;
			IShift<char> action;

			action = new Shift<char>(new MockedTerminalInput('a'), 1);
			state = new State<char>();
			state.Add(action);

			action=state.GetShift(new MockedTerminalInput('a'));
			Assert.IsNotNull(action);
		}
		[TestMethod]
		public void ShoudNotGetShiftUsingTerminalInput()
		{
			State<char> state;
			IShift<char> action;

			action = new Shift<char>(new MockedTerminalInput('a'), 1);
			state = new State<char>();
			state.Add(action);

			action = state.GetShift(new MockedTerminalInput('b'));
			Assert.IsNull(action);
		}

		[TestMethod]
		public void ShoudGetReduceUsingTerminalInput()
		{
			State<char> state;
			IReduce<char> action;

			action = new Reduce<char>("A", true, new MockedTerminalInput('a'));
			state = new State<char>();
			state.Add(action);

			action = state.GetReduce(new MockedTerminalInput('a'));
			Assert.IsNotNull(action);
		}
		[TestMethod]
		public void ShoudNotGetReduceUsingTerminalInput()
		{
			State<char> state;
			IReduce<char> action;

			action = new Reduce<char>("A", true, new MockedTerminalInput('a'));
			state = new State<char>();
			state.Add(action);

			action = state.GetReduce(new MockedTerminalInput('b'));
			Assert.IsNull(action);
		}

	


	}
}
