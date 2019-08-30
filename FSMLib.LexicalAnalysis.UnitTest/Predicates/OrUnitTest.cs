using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FFSMLib.LexicalAnalysis.UnitTest.Predicates
{
	[TestClass]
	public class OrUnitTest
	{


		[TestMethod]
		public void ShouldConvertToStringWithoutBullet()
		{
			Or predicate;
			Terminal terminal;

			terminal = new Terminal('a');
			predicate = new Or();
			predicate.Items.Add(new Terminal('a'));
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal('a'));
			Assert.AreEqual("(a|a|a)", predicate.ToString());

			predicate = new Or();
			predicate.Items.Add(terminal);
			Assert.AreEqual("a", predicate.ToString());
		}
		[TestMethod]
		public void ShouldConvertToStringWithBullet()
		{
			Or predicate;
			Terminal terminal;

			terminal = new Terminal('a');
			predicate = new Or();
			predicate.Items.Add(new Terminal('a'));
			predicate.Items.Add(terminal);
			predicate.Items.Add(new Terminal('a'));
			Assert.AreEqual("(a|•a|a)", predicate.ToString(terminal));

			predicate = new Or();
			predicate.Items.Add(terminal);
			Assert.AreEqual("•a", predicate.ToString(terminal));
		}




		


		[TestMethod]
		public void ShouldEquals()
		{
			Or a, b;


			a = new Or();
			a.Items.Add(new Terminal('a'));
			a.Items.Add(new Terminal('b'));
			a.Items.Add(new Terminal('c'));
			b = new Or();
			b.Items.Add(new Terminal('a'));
			b.Items.Add(new Terminal('b'));
			b.Items.Add(new Terminal('c'));

			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));

		}

		[TestMethod]
		public void ShouldNotEquals()
		{
			Or a, b;


			a = new Or();
			a.Items.Add(new Terminal('a'));
			a.Items.Add(new Terminal('b'));
			a.Items.Add(new Terminal('c'));
			b = new Or();
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
