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
	public class NonTerminal: LexicalPredicate, INonTerminalPredicate<char>
	{
		[XmlAttribute]
		public string Name
		{
			get;
			set;
		}

		public NonTerminal()
		{

		}
		public NonTerminal(string Name)
		{
			this.Name = Name;
		}

		public IInput<char> GetInput()
		{
			return new NonTerminalInput(Name);
		}
		public override string ToString()
		{
			return ToString(null);
		}
		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return $"•{{{Name}}}";
			else return $"{{{Name}}}";
		}


		public override bool Equals(IPredicate<char> other)
		{
			if (!(other is NonTerminal o)) return false;
			if (Name == null) return o.Name == null;
			return Name == o.Name;
		}

		public bool Match(char Input)
		{
			return false;
		}
		public bool Match(IInput<char> Input)
		{
			if (!(Input is NonTerminalInput o)) return false;
			if (Name == null) return o.Name == null;
			return Name == o.Name;
		}

	}
}
