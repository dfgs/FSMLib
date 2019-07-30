﻿using FSMLib.Inputs;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Predicates
{
	public class Reduce : LexicalPredicate, IReducePredicate<char>
	{

		public Reduce()
		{

		}


		public IInput<char> GetInput()
		{
			return null;
		}

		public override string ToString()
		{
			return ToString(null);
		}


		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (CurrentPredicate == this) return "•←";
			else return "←";
		}

		public override bool Equals(IPredicate<char> other)
		{
			return other is Reduce;
		}

		public bool Match(char Input)
		{
			return false;
		}

		public bool Match(IInput<char> Input)
		{
			return false;
		}

	}



}