using FSMLib.ActionTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Rules
{
	public class Situation<T>:IEquatable<Situation<T>>
	{
		public ActionTable<T> ActionTable
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
			return ((other.ActionTable == ActionTable) && (other.StateIndex == StateIndex));
		}

		public override string ToString()
		{
			return StateIndex.ToString();
		}

	}
}
