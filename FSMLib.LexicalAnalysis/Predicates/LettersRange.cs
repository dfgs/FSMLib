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
	public class LettersRange: ISituationPredicate<char>
	{
		[XmlAttribute]
		public char FirstValue
		{
			get;
			set;
		}
		[XmlAttribute]
		public char LastValue
		{
			get;
			set;
		}

		public  IInput<char> GetInput()
		{
			return new TerminalRangeInput<char>() { FirstValue = this.FirstValue, LastValue = this.LastValue };
		}

		public override string ToString()
		{
			return ToString(null);
		}

		public  string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•[{FirstValue}-{LastValue}]";
			else return $"[{FirstValue}-{LastValue}]";
		}


		public  bool Equals(IPredicate<char> other)
		{
			if (!(other is LettersRange o)) return false;
			return ((FirstValue==o.FirstValue) && (LastValue==o.LastValue));
		}

		public bool Match(char Input)
		{
			return (Input>=FirstValue) && (Input<=LastValue);
		}

		public bool Match(IInput<char> Input)
		{
			if (!(Input is TerminalInput<char> o)) return false;
			return (o.Value >= FirstValue) && (o.Value <= LastValue);
		}



	}
}
