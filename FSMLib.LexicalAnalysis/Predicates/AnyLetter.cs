﻿using FSMLib.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Predicates
{
	public class AnyLetter : ISituationPredicate<char>
	{
		public IInput<char> GetInput()
		{
			return new AnyTerminalInput<char>();
		}

		public override string ToString()
		{
			return ToString(null);
		}
		public string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•.";
			else return ".";
		}

		public bool Equals(IPredicate<char> other)
		{
			return other is AnyLetter;
		}

		public bool Match(char Input)
		{
			return true;
		}

		public bool Match(IInput<char> Input)
		{
			return Input is TerminalInput<char>;
		}



	}

}