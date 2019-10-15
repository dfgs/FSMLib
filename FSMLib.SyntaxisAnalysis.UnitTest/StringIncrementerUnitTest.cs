using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.SyntaxicAnalysis.UnitTest
{
	[TestClass]
	public class StringIncrementerUnitTest
	{
		

		[TestMethod]
		public void ShouldInc()
		{
			StringIncrementer s;
			s = new StringIncrementer();

			Assert.AreEqual("b", s.Inc("a"));
			Assert.AreEqual("ab", s.Inc("aa"));
			Assert.AreEqual("\0", s.Inc(string.Empty));
			Assert.AreEqual("\uFFFF\u0000", s.Inc("\uFFFF"));
		}

		[TestMethod]
		public void ShouldNotInc()
		{
			StringIncrementer s;
			s = new StringIncrementer();

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => s.Inc(null));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => s.Inc(Token.MaxStringValue));
		}
		[TestMethod]
		public void ShouldDec()
		{
			StringIncrementer s;
			s = new StringIncrementer();

			Assert.AreEqual("a", s.Dec("b"));
			Assert.AreEqual("aa", s.Dec("ab"));
			Assert.AreEqual(string.Empty, s.Dec("\0"));
			Assert.AreEqual("\uFFFF", s.Dec("\uFFFF\u0000"));
		}
		[TestMethod]
		public void ShouldNotDec()
		{
			StringIncrementer s;
			s = new StringIncrementer();

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => s.Dec(null));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => s.Dec(Token.MinStringValue));
		}




	}
}
