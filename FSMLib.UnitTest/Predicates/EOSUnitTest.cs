﻿using FSMLib.Inputs;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class EOSUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			EOS<char> predicate;

			predicate = new EOS<char>();

			Assert.AreEqual("¤", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			EOS<char> predicate;

			predicate = new EOS<char>();

			Assert.AreEqual("•¤", predicate.ToString(predicate));
		}

		[TestMethod]
		public void ShouldMatch()
		{
			EOS<char> predicate;


			predicate = new EOS<char>();

			Assert.IsTrue(predicate.Match(new EOSInput<char>()));

		}
		[TestMethod]
		public void ShouldNotMatch()
		{
			EOS<char> predicate;


			predicate = new EOS<char>();

			Assert.IsFalse(predicate.Match('b'));
			Assert.IsFalse(predicate.Match(new TerminalInput<char>() { Value = 'b' }));
			Assert.IsFalse(predicate.Match(new NonTerminalInput<char>() { Name = "a" }));

		}
		[TestMethod]
		public void ShouldGetInput()
		{
			EOS<char> predicate;
			IInput<char> input;

			predicate = new EOS<char>();
			input = predicate.GetInput();
			Assert.IsNotNull(input);
			Assert.IsInstanceOfType(input, typeof(EOSInput<char>));
		}



		[TestMethod]
		public void ShouldEquals()
		{
			EOS<char> a, b;


			a = new EOS<char>();
			b = new EOS<char>();

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			EOS<char> a;


			a = new EOS<char>();

			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new Terminal<char>()));
			Assert.IsFalse(a.Equals(new AnyTerminal<char>()));


		}




	}
}
