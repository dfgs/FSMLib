﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Inputs
{
	public class TerminalInput<T>:BaseInput<T>
	{
		public override int Priority => 0;

		public T Value
		{
			get;
			set;
		}

		

		public override bool Match(IInput<T> Other)
		{
			if (Other == null) return false;

			if (Other is TerminalInput<T> other)
			{
				if (Value == null) return other.Value == null;
				return Value.Equals(other.Value);
			}
			
			return false;
		}

		public override bool Match(T Other)
		{
			if (Other == null) return false;

			if (Value == null) return Other == null;
			return Value.Equals(Other);
		}
		public override bool Equals(IInput<T> other)
		{
			if (other == null) return false;
			if (other is TerminalInput<T> input)
			{
				if (Value == null) return input.Value == null; 
				return Value.Equals(input.Value);
			}
			return false;
		}

		public override string ToString()
		{
			return Value?.ToString();
		}


	}
}