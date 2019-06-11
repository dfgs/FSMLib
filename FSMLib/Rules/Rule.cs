using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Rules
{
	[System.Serializable]
    public class Rule<T>:IEquatable<Rule<T>>
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
		
		public BasePredicate<T> Predicate
		{
			get;
			set;
		}

		

		public override string ToString()
		{
			return $"{Name}{(IsAxiom ? "*" : "")}={Predicate.ToString()}";
		}

		public string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			if (CurrentPredicate is ReducePredicate<T>) return $"{Name}{(IsAxiom?"*":"")}={Predicate.ToString()}•";
			return $"{Name}{(IsAxiom ? "*" : "")}={Predicate.ToString(CurrentPredicate)}";
		}

		public bool Equals(Rule<T> other)
		{
			if (other == null) return false;
			if ((other.Name != this.Name) || (other.IsAxiom != this.IsAxiom)) return false;
			if (Predicate == null) return other.Predicate == null;
			return Predicate.Equals(other.Predicate);
		}


	}
}
