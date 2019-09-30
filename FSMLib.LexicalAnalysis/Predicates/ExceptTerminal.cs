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
	public class ExceptTerminal: LexicalPredicate, ISituationPredicate<char>
	{
		[XmlAttribute]
		public char Value
		{
			get;
			set;
		}

		public ExceptTerminal()
		{
			this.Value = (char)0;
		}

		public ExceptTerminal(char Value)
		{
			this.Value = Value;
		}

		public IEnumerable<IInput<char>> GetInputs()
		{
			if (Value == char.MinValue) yield return new TerminalRangeInput((char)(char.MinValue + 1), char.MaxValue);
			else if (Value == char.MaxValue) yield return new TerminalRangeInput(char.MinValue, (char)(char.MaxValue - 1));
			else
			{
				yield return new TerminalRangeInput(char.MinValue, (char)(Value - 1));
				yield return new TerminalRangeInput((char)(Value + 1),char.MaxValue);

			}
		}

		public override string ToString()
		{
			return ToString(null);
		}

		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•!{Value}";
			else return $"!{Value.ToString()}";
		}


		public override  bool Equals(IPredicate<char> other)
		{
			if (!(other is ExceptTerminal letter)) return false;
			return Value==letter.Value;
		}

		

	}
}
