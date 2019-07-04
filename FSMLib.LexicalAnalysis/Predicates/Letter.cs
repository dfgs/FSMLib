using FSMLib.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.LexicalAnalysis.Predicates
{
	public class Letter:ISituationPredicate<char>
	{
		[XmlAttribute]
		public char Value
		{
			get;
			set;
		}

		public Letter(char Value)
		{
			this.Value = Value;
		}

		public  IInput<char> GetInput()
		{
			return new TerminalInput<char>() { Value = Value };
		}

		public override string ToString()
		{
			return ToString(null);
		}

		public string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•{Value}";
			else return Value.ToString();
		}


		public  bool Equals(IPredicate<char> other)
		{
			if (!(other is Letter letter)) return false;
			return Value==letter.Value;
		}

		public bool Match(char Input)
		{
			return Input==Value;
		}
		public bool Match(IInput<char> Input)
		{
			if (!(Input is TerminalInput<char> o)) return false;
			return (o.Value == Value);
		}

	}
}
