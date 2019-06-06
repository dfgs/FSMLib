﻿using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class AnyTerminalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			AnyTerminal<char> predicate;

			predicate = new AnyTerminal<char>();

			Assert.AreEqual(".", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			AnyTerminal<char> predicate;

			predicate = new AnyTerminal<char>();

			Assert.AreEqual("•.", predicate.ToString(predicate));
		}

		[TestMethod]
		public void ShouldGetInput()
		{
			AnyTerminal<char> predicate;
			BaseInput<char>[] inputs;

			predicate = new AnyTerminal<char>();
			inputs = predicate.GetInputs().ToArray();
			Assert.AreEqual(1, inputs.Length);
			Assert.IsInstanceOfType(inputs[0], typeof(AnyTerminalInput<char>));
		}







	}
}
