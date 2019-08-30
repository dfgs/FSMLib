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
	public class Letter: LexicalPredicate, ISituationPredicate<char>
	{
		[XmlAttribute]
		public char Value
		{
			get;
			set;
		}

		public Letter()
		{
			this.Value = (char)0;
		}

		public Letter(char Value)
		{
			this.Value = Value;
		}

		public  IEnumerable<IInput<char>> GetInputs()
		{
			yield return new LetterInput(Value);
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


		public override  bool Equals(IPredicate<char> other)
		{
			if (!(other is Letter letter)) return false;
			return Value==letter.Value;
		}

		

	}
}
