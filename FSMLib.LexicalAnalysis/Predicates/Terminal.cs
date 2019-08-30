using FSMLib.Inputs;
using FSMLib.LexicalAnalysis.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.LexicalAnalysis.Predicates
{
	public class Terminal: LexicalPredicate, ITerminalPredicate<char>
	{
		[XmlAttribute]
		public char Value
		{
			get;
			set;
		}

		// for serialisation
		public Terminal()
		{

		}
		public Terminal(char Value)
		{
			this.Value = Value;
		}

		public IEnumerable<IInput<char>> GetInputs()
		{
			yield return new TerminalInput(Value);
		}


		public override string ToString()
		{
			return ToString(null);
		}

		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•{Value}";
			else return Value.ToString();
		}


		public override bool Equals(IPredicate<char> other)
		{
			if (!(other is ITerminalPredicate<char> terminal)) return false;
			return Value.Equals(terminal.Value);
		}




	}
}
