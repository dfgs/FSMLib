﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

		[TestMethod]
		public void ShouldSerializeAndDeserializeComplexRules()
		{
			List<Rule<char>> rules;
			Rule<char>[] results;

			RuleCollectionSerializer<char> serializer;
			MemoryStream stream;

			rules = new List<Rule<char>>();
			rules.Add(RuleHelper.BuildRule("A*=a*{B}+c?."));
			rules.Add(RuleHelper.BuildRule("B={B}|c*|d+|e|f"));

			stream = new MemoryStream();

			serializer = new RuleCollectionSerializer<char>();
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
			List<Rule<char>> rules;
			Rule<char>[] results;

			RuleCollectionSerializer<char> serializer;

			rules = new List<Rule<char>>();
			rules.Add(RuleHelper.BuildRule("A*=a*{B}+c?."));
			rules.Add(RuleHelper.BuildRule("B={B}|c*|d+|e|f"));

			serializer = new RuleCollectionSerializer<char>();

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
