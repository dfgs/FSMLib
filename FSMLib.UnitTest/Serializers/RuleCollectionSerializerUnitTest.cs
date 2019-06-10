using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FSMLib.Helpers;
using FSMLib.Rules;
using FSMLib.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSMLib.UnitTest.Serializers
{
	[TestClass]
	public class RuleCollectionSerializerUnitTest
	{
		[TestMethod]
		public void ShouldSerializeAndDeserializeTerminalRules()
		{
			List<Rule<char>> rules;
			Rule<char>[] results;

			RuleCollectionSerializer<char> serializer;
			MemoryStream stream;

			rules = new List<Rule<char>>();
			rules.Add(RuleHelper.BuildRule("A*=a"));
			rules.Add(RuleHelper.BuildRule("B=b"));

			stream = new MemoryStream();

			serializer = new RuleCollectionSerializer<char>();
			serializer.SaveToStream(stream, rules);
			stream.Position = 0;
			results=serializer.LoadStream(stream).ToArray();

			Assert.AreEqual(2, results.Length);
			Assert.IsTrue(rules.IsIndenticalToEx(results));
		}

		[TestMethod]
		public void ShouldSerializeAndDeserializeSequenceRules()
		{
			List<Rule<char>> rules;
			Rule<char>[] results;

			RuleCollectionSerializer<char> serializer;
			MemoryStream stream;

			rules = new List<Rule<char>>();
			rules.Add(RuleHelper.BuildRule("A*=abc"));
			rules.Add(RuleHelper.BuildRule("B=bcdef"));

			stream = new MemoryStream();

			serializer = new RuleCollectionSerializer<char>();
			serializer.SaveToStream(stream, rules);
			stream.Position = 0;
			results = serializer.LoadStream(stream).ToArray();

			Assert.AreEqual(2, results.Length);
			Assert.IsTrue(rules.IsIndenticalToEx(results));
		}

	}
}
