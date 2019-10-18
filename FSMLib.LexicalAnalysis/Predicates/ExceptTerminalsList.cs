using FSMLib.Common;
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
	public class ExceptTerminalsList: LexicalPredicate, ISituationPredicate<char>
	{
		[XmlArray]
		public List<char> Values
		{
			get;
			set;
		}
		


		public ExceptTerminalsList()
		{
			this.Values = new List<char>();
		}
		public ExceptTerminalsList(IEnumerable<char> Values)
		{
			this.Values = new List<char>(Values);
		}
		public ExceptTerminalsList(params char[] Values)
		{
			this.Values = new List<char>(Values);
		}

		public IEnumerable<IInput<char>> GetInputs()
		{
			TerminalRangeInputCollection<char> rangeCollection;


			rangeCollection = new TerminalRangeInputCollection<char>(new RangeValueProvider());
			rangeCollection.Add(new TerminalRangeInput(char.MinValue, char.MaxValue));


			if (Values != null) foreach (char val in Values )
			{
				rangeCollection.Add(val);
			}

			foreach(TerminalRangeInput input in rangeCollection)
			{
				//if (input.FirstValue != input.LastValue) yield return input;
				if (!Values.Contains(input.FirstValue)) yield return input;
			}

			
		}

		public override string ToString()
		{
			return ToString(null);
		}

		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•![{string.Join(",",Values)}]";
			else return $"![{string.Join(",", Values)}]";
		}


		public override  bool Equals(IPredicate<char> other)
		{
			if (!(other is ExceptTerminalsList o)) return false;
			if (o.Values == null) return Values == null;
			if (o.Values.Count != Values.Count) return false;
			foreach(char val in o.Values)
			{
				if (!Values.Contains(val)) return false;
			}
			return true;
		}

	



	}
}
