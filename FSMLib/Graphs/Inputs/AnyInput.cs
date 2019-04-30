using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs.Inputs
{
	public class AnyInput<T> : BaseInput<T>
	{
		private static int hashCode = new Random().Next();

		public override bool Match(IInput<T> Other)
		{
			return Other != null;
		}

		public override bool Match(T Other)
		{
			return Other != null;
		}

		public override bool Equals(IInput<T> other)
		{
			if (other == null) return false;
			return (other is AnyInput<T>);
		}

		public override int GetHashCode()
		{
			return hashCode;
		}

	}
}
