using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FSMLib.Predicates;
using FSMLib.Rules;

namespace FSMLib.Serializers
{
	public class RuleCollectionSerializer<T> : IRuleCollectionSerializer<T>
	{
		private static Type[] extraTypes=new Type[] { typeof(Terminal<T>),typeof(AnyTerminal<T>), typeof(NonTerminal<T>), typeof(EOS<T>), typeof(OneOrMore<T>), typeof(ZeroOrMore<T>),typeof(OneOrMore<T>), typeof(Optional<T>), typeof(Or<T>), typeof(Sequence<T>),typeof(TerminalRange<T>) } ;
		public IEnumerable<Rule<T>> LoadStream(Stream Stream)
		{
			XmlSerializer serializer;
			serializer = new XmlSerializer(typeof(List<Rule<T>>), extraTypes);
			return serializer.Deserialize(Stream) as IEnumerable<Rule<T>>;
		}

		public void SaveToStream(Stream Stream, IEnumerable<Rule<T>> Rules)
		{
			XmlSerializer serializer;
			serializer = new XmlSerializer(typeof(Rule<T>[]),extraTypes) ;
			serializer.Serialize(Stream, Rules.ToArray());
		}
	}
}
