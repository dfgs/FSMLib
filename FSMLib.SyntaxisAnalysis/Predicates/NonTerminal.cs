using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FSMLib.SyntaxisAnalysis;

namespace FSMLib.SyntaxicAnalysis.Predicates
{
	public class NonTerminal : SyntaxicPredicate, INonTerminalPredicate<Token>
	{
		[XmlAttribute]
		public string Name
		{
			get;
			set;
		}


		public NonTerminal(string Name)
		{
			this.Name = Name;
		}

		public IEnumerable<IInput<Token>> GetInputs()
		{
			yield return new NonTerminalInput(Name);
		}

		public override string ToString()
		{
			return ToString(null);
		}
		public override string ToString(ISituationPredicate<Token> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•{{{Name}}}";
			else return $"{{{Name}}}";
		}


		public override bool Equals(IPredicate<Token> other)
		{
			if (!(other is INonTerminalPredicate<Token> o)) return false;
			if (Name == null) return o.Name == null;
			return Name == o.Name;
		}


	}

}
