using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.SyntaxisAnalysis.UnitTest
{
	[TestClass]
	public class TokenUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new Token(null, "A"));
			Assert.ThrowsException<ArgumentNullException>(() => new Token("A",null));
		}

		[TestMethod]
		public void ShouldEquals()
		{
			Token a,b;

			a = new Token("C", "V"); 
			b = new Token("C", "V");

			Assert.IsTrue(a == b);
			Assert.IsFalse(a != b);
			Assert.IsTrue(a.Equals(b));
			Assert.IsTrue(b.Equals(a));
		}

		[TestMethod]
		public void ShouldNotEqualsWhenValueIsDifferent()
		{
			Token a, b;

			a = new Token("C", "V");
			b = new Token("C", "v");

			Assert.IsFalse(a == b);
			Assert.IsTrue(a != b);
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
		}
		[TestMethod]
		public void ShouldNotEqualsWhenClassIsDifferent()
		{
			Token a, b;

			a = new Token("C", "V");
			b = new Token("c", "V");

			Assert.IsFalse(a == b);
			Assert.IsTrue(a != b);
			Assert.IsFalse(a.Equals(b));
			Assert.IsFalse(b.Equals(a));
		}
		[TestMethod]
		public void ShouldReturnsSameHashCode()
		{
			Token a, b;

			a = new Token("C", "V");
			b = new Token("C", "V");

			Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
		}
		[TestMethod]
		public void ShouldConvertToString()
		{
			Token a;

			a = new Token("C", "V");

			Assert.AreEqual("<C,V>",a.ToString());
		}


	}
}
