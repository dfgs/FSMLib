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
		private static Type[] extraTypes=new Type[] { typeof(TerminalPredicate<T>),typeof(AnyTerminalPredicate<T>), typeof(NonTerminalPredicate<T>), typeof(EOSPredicate<T>), typeof(OneOrMorePredicate<T>), typeof(ZeroOrMorePredicate<T>),typeof(OneOrMorePredicate<T>), typeof(OptionalPredicate<T>), typeof(OrPredicate<T>), typeof(SequencePredicate<T>),typeof(TerminalRangePredicate<T>) } ;
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
