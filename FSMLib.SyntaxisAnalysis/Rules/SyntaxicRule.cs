using FSMLib.SyntaxicAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FSMLib.SyntaxisAnalysis;

namespace FSMLib.SyntaxicAnalysis.Rules
{
	public class SyntaxicRule:IRule<Token>
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
		IPredicate<Token> IRule<Token>.Predicate => Predicate;

		public SyntaxicPredicate Predicate
		{
			get;
			set;
		}



		public override string ToString()
		{
			return $"{Name}{(IsAxiom ? "*" : "")}={Predicate.ToString()}";
		}

		public string ToString(ISituationPredicate<Token> CurrentPredicate)
		{
			if (CurrentPredicate is IReducePredicate<Token>) return $"{Name}{(IsAxiom ? "*" : "")}={Predicate.ToString()}•";
			return $"{Name}{(IsAxiom ? "*" : "")}={Predicate.ToString(CurrentPredicate)}";
		}

		public bool Equals(IRule<Token> other)
		{
			if (other == null) return false;
			if ((other.Name != this.Name) || (other.IsAxiom != this.IsAxiom)) return false;
			if (Predicate == null) return other.Predicate == null;
			return Predicate.Equals(other.Predicate);
		}

		
	}
}
