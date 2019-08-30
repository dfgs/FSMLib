using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Rules;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.Serializers;

namespace FSMLib.LexicalAnalysis.Serializers
{
	public class RuleCollectionSerializer : IRuleCollectionSerializer<char>
	{
		private static Type[] extraTypes=new Type[] { typeof(Terminal),typeof(AnyTerminal), typeof(NonTerminal), typeof(EOS), typeof(OneOrMore), typeof(ZeroOrMore), typeof(Optional), typeof(Or), typeof(Sequence),typeof(TerminalsRange),typeof(Reduce) } ;
		public IEnumerable<IRule<char>> LoadStream(Stream Stream)
		{
			XmlSerializer serializer;
			serializer = new XmlSerializer(typeof(List<LexicalRule>), extraTypes);
			return serializer.Deserialize(Stream) as IEnumerable<IRule<char>>;
		}

		public void SaveToStream(Stream Stream, IEnumerable<IRule<char>> Rules)
		{
			XmlSerializer serializer;
			serializer = new XmlSerializer(typeof(LexicalRule[]),extraTypes) ;
			serializer.Serialize(Stream, Rules.Cast<LexicalRule>().ToArray());
		}
	}
}
