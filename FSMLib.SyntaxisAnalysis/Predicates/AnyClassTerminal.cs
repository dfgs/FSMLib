using FSMLib.Inputs;
using FSMLib.SyntaxicAnalysis.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.SyntaxicAnalysis;
using System.Xml.Serialization;

namespace FSMLib.SyntaxicAnalysis.Predicates
{
	public class AnyClassTerminal : SyntaxicPredicate, ISituationPredicate<Token>
	{
		[XmlAttribute]
		public string Class
		{
			get;
			set;
		}

		public AnyClassTerminal()
		{

		}

		public AnyClassTerminal(string Class)
		{
			this.Class = Class;
		}

		public IEnumerable<IInput<Token>> GetInputs()
		{
			yield return new TerminalRangeInput(new Token(Class,Token.MinStringValue),new Token(Class,Token.MaxStringValue));
		}

		public override string ToString()
		{
			return ToString(null);
		}
		public override string ToString(ISituationPredicate<Token> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•<{Class}>";
			else return $"<{Class}>";
		}

		public override bool Equals(IPredicate<Token> other)
		{
			if (other == null) return false;
			if (!(other is AnyClassTerminal o)) return false;
			return o.Class == this.Class;
		}

		



	}

}