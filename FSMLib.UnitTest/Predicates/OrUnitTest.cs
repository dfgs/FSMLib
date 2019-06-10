﻿using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.UnitTest.Predicates
{
	[TestClass]
	public class OrUnitTest
	{


		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Or<char> predicate;
			Terminal<char> terminal;

			terminal = new Terminal<char>() { Value = 'a' };
			predicate = new Or<char>();
			predicate.Items.Add(new Terminal<char>() { Value = 'a' });
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal<char>() { Value = 'a' });
			Assert.AreEqual("(a|a|a)", predicate.ToString());

			predicate = new Or<char>();
			predicate.Items.Add(terminal);
			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Or<char> predicate;
			Terminal<char> terminal;

			terminal = new Terminal<char>() { Value = 'a' };
			predicate = new Or<char>();
			predicate.Items.Add(new Terminal<char>() { Value = 'a' });
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal<char>() { Value = 'a' });
			Assert.AreEqual("(a|•a|a)", predicate.ToString(terminal));

			predicate = new Or<char>();
			predicate.Items.Add(terminal);
			Assert.AreEqual("•a", predicate.ToString(terminal));
		}




		[TestMethod]
		public void ShouldConvertImplicitelyFromPredicateArray()
		{
			Sequence<char> predicate;

			predicate = new BasePredicate<char>[] { (Terminal<char>)'a', (Terminal<char>)'b', (Terminal<char>)'c' };
			Assert.IsNotNull(predicate);
			Assert.AreEqual(3, predicate.Items.Count);

		}


		[TestMethod]
		public void ShouldConvertImplicitelyFromValueArray()
		{
			Or<char> predicate;

			predicate = new char[] { 'a', 'b', 'c' };
			Assert.IsNotNull(predicate);
			Assert.AreEqual(3, predicate.Items.Count);

		}


		[TestMethod]
		public void ShouldEquals()
		{
			Or<char> a, b;


			a = new Or<char>();
			a.Items.Add((Terminal<char>)'a');
			a.Items.Add((Terminal<char>)'b');
			a.Items.Add((Terminal<char>)'c');
			b = new Or<char>();
			b.Items.Add((Terminal<char>)'a');
			b.Items.Add((Terminal<char>)'b');
			b.Items.Add((Terminal<char>)'c');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Or<char> a, b;


			a = new Or<char>();
			a.Items.Add((Terminal<char>)'a');
			a.Items.Add((Terminal<char>)'b');
			a.Items.Add((Terminal<char>)'c');
			b = new Or<char>();
			b.Items.Add((Terminal<char>)'a');
			b.Items.Add((Terminal<char>)'c');
			b.Items.Add((Terminal<char>)'b');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyTerminal<char>()));
			Assert.IsFalse(a.Equals(new EOS<char>()));


		}

	}
}
