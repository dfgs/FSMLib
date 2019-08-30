using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FSMLib.LexicalAnalysis.UnitTest.Predicates
{
	[TestClass]
	public class OptionalUnitTest
	{
		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Optional predicate;
			Sequence sequence;
			Terminal terminal;

			terminal = new Terminal('a');
			predicate = new Optional();
			predicate.Item = terminal;

			Assert.AreEqual("a?", predicate.ToString());

			sequence = new Sequence();
			sequence.Items.Add(new Terminal('a'));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal('a'));
			predicate = new Optional();
			predicate.Item = sequence;
			Assert.AreEqual("(aaa)?", predicate.ToString());

		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Optional predicate;
			Sequence sequence;
			Terminal terminal;

			terminal = new Terminal('a');
			predicate = new Optional();
			predicate.Item = terminal;

			Assert.AreEqual("•a?", predicate.ToString(terminal));

			sequence = new Sequence();
			sequence.Items.Add(new Terminal('a'));
			sequence.Items.Add(terminal);
			sequence.Items.Add(new Terminal('a'));
			predicate = new Optional();
			predicate.Item = sequence;
			Assert.AreEqual("(a•aa)?", predicate.ToString(terminal));
		}



		[TestMethod]
		public void ShouldEquals()
		{
			Optional a, b;


			a = new Optional();
			a.Item = new Terminal('a');
			b = new Optional();
			b.Item = new Terminal('a');

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Optional a, b;


			a = new Optional();
			a.Item =  new Terminal('a');
			b = new Optional();
			b.Item =  new Terminal('b');

			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(a.Equals(null));
			Assert.IsFalse(a.Equals(new AnyTerminal()));
			Assert.IsFalse(a.Equals(new EOS()));


		}



	}
}
