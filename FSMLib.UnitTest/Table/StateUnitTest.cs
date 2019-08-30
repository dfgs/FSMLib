using System;
using System.Linq;
using FSMLib.Table;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSMLib.Situations;
using FSMLib.Actions;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Common.Table;
using FSMLib.Common.Actions;

namespace FSMLib.UnitTest.AutomatonTables
{
	[TestClass]
	public class StateUnitTest
	{
		[TestMethod]
		public void ShoudAddShift()
		{
			State<char> state;
			Shift<char> action;

			action = new Shift<char>();
			action.Input = new TerminalInput('a');
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

			action = new Reduce<char>();
			action.Input = new TerminalInput('a');
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

			action = new Shift<char>();
			action.Input = new TerminalInput('a');
			state = new State<char>();
			state.Add(action);

			action=state.GetShift(new TerminalInput('a'));
			Assert.IsNotNull(action);
		}
		[TestMethod]
		public void ShoudNotGetShiftUsingTerminalInput()
		{
			State<char> state;
			IShift<char> action;

			action = new Shift<char>();
			action.Input = new TerminalInput('a');
			state = new State<char>();
			state.Add(action);

			action = state.GetShift(new TerminalInput('b'));
			Assert.IsNull(action);
		}

		[TestMethod]
		public void ShoudGetReduceUsingTerminalInput()
		{
			State<char> state;
			IReduce<char> action;

			action = new Reduce<char>();
			action.Input = new TerminalInput('a');
			state = new State<char>();
			state.Add(action);

			action = state.GetReduce(new TerminalInput('a'));
			Assert.IsNotNull(action);
		}
		[TestMethod]
		public void ShoudNotGetReduceUsingTerminalInput()
		{
			State<char> state;
			IReduce<char> action;

			action = new Reduce<char>();
			action.Input = new TerminalInput('a');
			state = new State<char>();
			state.Add(action);

			action = state.GetReduce(new TerminalInput('b'));
			Assert.IsNull(action);
		}

		[TestMethod]
		public void ShoudGetShiftUsingAnyTerminalInput()
		{
			State<char> state;
			IShift<char> action;

			action = new Shift<char>();
			action.Input = new TerminalsRangeInput(char.MinValue,char.MaxValue);
			state = new State<char>();
			state.Add(action);

			action = state.GetShift(new TerminalInput('a'));
			Assert.IsNotNull(action);
			action = state.GetShift(new TerminalsRangeInput(char.MinValue, char.MaxValue));
			Assert.IsNotNull(action);
		}
		[TestMethod]
		public void ShoudNotGetShiftUsingAnyTerminalInput()
		{
			State<char> state;
			IShift<char> action;

			action = new Shift<char>();
			action.Input = new TerminalsRangeInput(char.MinValue, char.MaxValue);
			state = new State<char>();
			state.Add(action);

			action = state.GetShift(new NonTerminalInput("A"));
			Assert.IsNull(action);
			
		}


	}
}
