using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public class NonTerminalInput<T>:BaseInput<T>,IEquatable<NonTerminalInput<T>>//,IEquatable<BaseInput<T>>
	{
		public string Name
		{
			get;
			set;
		}



		public bool Equals(NonTerminalInput<T> other)
		{
			if (other == null) return false;
			return other.Name == Name;
		}

		

		public override bool Match(IInput<T> Other)
		{
			if (Other == null) return false;
			if (Other is NonTerminalInput<T> nonTerminalInput) return nonTerminalInput.Name==Name;
			
			return false;
		}

	

		public override string ToString()
		{
			return $"{{{Name}}}";
		}

		
	}
}
