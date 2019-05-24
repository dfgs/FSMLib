using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Actions
{
	public class Reduce<T>:BaseAction<T>,IEquatable<Reduce<T>>
	{

		
		public string Name
		{
			get;
			set;
		}

		
		
		public List<ReductionTarget<T>> Targets
		{
			get;
			set;
		}

		public Reduce()
		{
			Targets = new List<ReductionTarget<T>>();
		}

		public bool Equals(Reduce<T> other)
		{
			if (other == null) return false;
			return other.Name == Name;
		}

	}
}
