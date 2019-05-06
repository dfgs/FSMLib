using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Inputs
{
	public class NonTerminalInput<T>:BaseInput<T>
	{
		public override int Priority => 10;

		public string Name
		{
			get;
			set;
		}

		

		public override bool Match(IInput<T> Other)
		{
			if (Other == null) return false;

			if (Other is NonTerminalInput<T> other)
			{
				if (Name == null) return other.Name == null;
				return Name.Equals(other.Name);
			}
			
			return false;
		}

		public override bool Match(T Other)
		{
			return false;
		}
		public override bool Equals(IInput<T> other)
		{
			if (other == null) return false;
			if (other is NonTerminalInput<T> input)
			{
				if (Name == null) return input.Name== null; 
				return Name.Equals(input.Name);
			}
			return false;
		}

		public override string ToString()
		{
			return Name?.ToString();
		}


	}
}
