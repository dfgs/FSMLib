using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Rules
{
	public class Situation<T>:IEquatable<Situation<T>>
	{
		public AutomatonTable<T> AutomatonTable
		{
			get;
			set;
		}

		public int StateIndex
		{
			get;
			set;
		}

		public bool Equals(Situation<T> other)
		{
			if (other == null) return false;
			return ((other.AutomatonTable == AutomatonTable) && (other.StateIndex == StateIndex));
		}

		public override string ToString()
		{
			return StateIndex.ToString();
		}

	}
}
