using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.LexicalAnalysis.Rules
{
	public class LexicalRule:IRule<char>
	{
		[XmlAttribute]
		public string Name
		{
			get;
			set;
		}

		[XmlAttribute]
		public bool IsAxiom
		{
			get;
			set;
		}

		[XmlIgnore]
		IPredicate<char> IRule<char>.Predicate => Predicate;

		public LexicalPredicate Predicate
		{
			get;
			set;
		}



		public override string ToString()
		{
			return $"{Name}{(IsAxiom ? "*" : "")}={Predicate.ToString()}";
		}

		public string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate is IReducePredicate<char>) return $"{Name}{(IsAxiom ? "*" : "")}={Predicate.ToString()}•";
			return $"{Name}{(IsAxiom ? "*" : "")}={Predicate.ToString(CurrentPredicate)}";
		}

		public bool Equals(IRule<char> other)
		{
			if (other == null) return false;
			if ((other.Name != this.Name) || (other.IsAxiom != this.IsAxiom)) return false;
			if (Predicate == null) return other.Predicate == null;
			return Predicate.Equals(other.Predicate);
		}

		
	}
}
