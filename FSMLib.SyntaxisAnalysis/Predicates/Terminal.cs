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
	public class Terminal : SyntaxicPredicate, ITerminalPredicate<Token>
	{
		[XmlAttribute]
		public Token Value
		{
			get;
			set;
		}



		public Terminal(Token Value)
		{
			this.Value = Value;
		}

		public IEnumerable<IInput<Token>> GetInputs()
		{
			yield return new TerminalInput(Value);
		}


		public override string ToString()
		{
			return ToString(null);
		}

		public override string ToString(ISituationPredicate<Token> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•{Value}";
			else return Value.ToString();
		}


		public override bool Equals(IPredicate<Token> other)
		{
			if (!(other is ITerminalPredicate<Token> terminal)) return false;
			return Value.Equals(terminal.Value);
		}




	}
}
