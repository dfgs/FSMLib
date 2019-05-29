using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public class NonTerminalInput<T>:BaseTerminalInput<T>//,IEquatable<BaseInput<T>>
	{
		public string Name
		{
			get;
			set;
		}



		public override bool Equals(BaseTerminalInput<T> other)
		{
			if (!(other is NonTerminalInput<T> o)) return false;
			return o.Name == Name;
		}

		

		public override bool Match(IInput<T> Other)
		{
			if (Other == null) return false;
			if (Other is NonTerminalInput<T> nonTerminalInput) return nonTerminalInput.Name==Name;
			
			return false;
		}

		

		public override bool Match(T Value)
		{
			return false;
		}

		public override string ToString()
		{
			return $"{{{Name}}}";
		}

		
	}
}
