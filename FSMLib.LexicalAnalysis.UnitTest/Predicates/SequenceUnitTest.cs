using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.LexicalAnalysis.UnitTest.Predicates
{
	[TestClass]
	public class SequenceUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Sequence predicate;
			Terminal terminal;

			terminal = new Terminal('a');
			predicate = new Sequence();
			predicate.Items.Add(new Terminal('a'));
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal('a'));
			Assert.AreEqual("(aaa)", predicate.ToString());

			predicate = new Sequence();
			predicate.Items.Add(terminal);
			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Sequence predicate;
			Terminal terminal;

			terminal = new Terminal('a');
			predicate = new Sequence();
			predicate.Items.Add(new Terminal('a'));
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal('a'));
			Assert.AreEqual("(a•aa)", predicate.ToString(terminal));

			predicate = new Sequence();
			predicate.Items.Add(terminal);
			Assert.AreEqual("•a", predicate.ToString(terminal));
		}


		

		[TestMethod]
		public void ShouldEquals()
		{
			Sequence a, b;


			a = new Sequence();
			a.Items.Add(new Terminal('a'));
			a.Items.Add(new Terminal('b'));
			a.Items.Add(new Terminal('c'));
			b = new Sequence();
			b.Items.Add(new Terminal('a'));
			b.Items.Add(new Terminal('b'));
			b.Items.Add(new Terminal('c'));

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Sequence a, b;


			a = new Sequence();
			a.Items.Add(new Terminal('a'));
			a.Items.Add(new Terminal('b'));
			a.Items.Add(new Terminal('c'));
			b = new Sequence();
			b.Items.Add(new Terminal('a'));
			b.Items.Add(new Terminal('c'));
			b.Items.Add(new Terminal('b'));

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyTerminal()));
			Assert.IsFalse(a.Equals(new EOS()));


		}



	}
}
