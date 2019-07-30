using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FSMLib.Helpers;
using FSMLib.LexicalAnalysis.Serializers;
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
			List<IRule<char>> rules;
			IRule<char>[] results;

			RuleCollectionSerializer serializer;
			MemoryStream stream;

			rules = new List<IRule<char>>();
			rules.Add(RuleHelper.BuildRule("A*=a"));
			rules.Add(RuleHelper.BuildRule("B=b"));

			stream = new MemoryStream();

			serializer = new RuleCollectionSerializer();
			serializer.SaveToStream(stream, rules);
			stream.Position = 0;
			results=serializer.LoadStream(stream).ToArray();

			Assert.AreEqual(2, results.Length);
			Assert.IsTrue(rules.IsIndenticalToEx(results));
		}

		[TestMethod]
		public void ShouldSerializeAndDeserializeSequenceRules()
		{
			List<IRule<char>> rules;
			IRule<char>[] results;

			RuleCollectionSerializer serializer;
			MemoryStream stream;

			rules = new List<IRule<char>>();
			rules.Add(RuleHelper.BuildRule("A*=abc"));
			rules.Add(RuleHelper.BuildRule("B=bcdef"));

			stream = new MemoryStream();

			serializer = new RuleCollectionSerializer();
			serializer.SaveToStream(stream, rules);
			stream.Position = 0;
			results = serializer.LoadStream(stream).ToArray();

			Assert.AreEqual(2, results.Length);
			Assert.IsTrue(rules.IsIndenticalToEx(results));
		}

		[TestMethod]
		public void ShouldSerializeAndDeserializeComplexRules()
		{
			List<IRule<char>> rules;
			IRule<char>[] results;

			RuleCollectionSerializer serializer;
			MemoryStream stream;

			rules = new List<IRule<char>>();
			rules.Add(RuleHelper.BuildRule("A*=a*{B}+c?."));
			rules.Add(RuleHelper.BuildRule("B={B}|c*|d+|e|f"));

			stream = new MemoryStream();

			serializer = new RuleCollectionSerializer();
			//serializer.SaveToStream(new FileStream(@"d:\test.xml", FileMode.Create), rules);


			serializer.SaveToStream(stream, rules);
			stream.Position = 0;
			results = serializer.LoadStream(stream).ToArray();

			Assert.AreEqual(2, results.Length);
			Assert.IsTrue(rules.IsIndenticalToEx(results));

		}

		[TestMethod]
		public void ShouldDeserializeFromResource()
		{
			List<IRule<char>> rules;
			IRule<char>[] results;

			RuleCollectionSerializer serializer;

			rules = new List<IRule<char>>();
			rules.Add(RuleHelper.BuildRule("A*=a*{B}+c?."));
			rules.Add(RuleHelper.BuildRule("B={B}|c*|d+|e|f"));

			serializer = new RuleCollectionSerializer();
			//serializer.SaveToStream(new FileStream(@"d:\test.xml", FileMode.Create), rules);

			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "FSMLib.UnitTest.Resources.test.xml";

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				results = serializer.LoadStream(stream).ToArray();
			}

			Assert.AreEqual(2, results.Length);
			Assert.IsTrue(rules.IsIndenticalToEx(results));

		}

	}
}
