
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Actions
{
	public class Accept<T>:BaseAction<T>,IEquatable<Accept<T>>
	{
		public string Name
		{
			get;
			set;
		}

		public bool Equals(Accept<T> other)
		{
			if (other == null) return false;
			return Name == other.Name;
		}


	}
}
