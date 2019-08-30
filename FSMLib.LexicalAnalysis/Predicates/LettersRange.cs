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
	public class LettersRange: LexicalPredicate, ISituationPredicate<char>
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


		public LettersRange()
		{
			this.FirstValue = (char)0; this.LastValue = (char)0;
		}
		public LettersRange(char FirstValue, char LastValue)
		{
			this.FirstValue = FirstValue; this.LastValue = LastValue;
		}

		public IEnumerable<IInput<char>> GetInputs()
		{
			yield return new LettersRangeInput(this.FirstValue, this.LastValue );
		}

		public override string ToString()
		{
			return ToString(null);
		}

		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•[{FirstValue}-{LastValue}]";
			else return $"[{FirstValue}-{LastValue}]";
		}


		public override  bool Equals(IPredicate<char> other)
		{
			if (!(other is LettersRange o)) return false;
			return ((FirstValue==o.FirstValue) && (LastValue==o.LastValue));
		}

	



	}
}
