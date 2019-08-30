using FSMLib.Actions;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Actions
{
	public class Reduce<T>:IReduce<T>
	{

		public string Name
		{
			get;
			set;
		}

		public bool IsAxiom
		{
			get;
			set;
		}

		public IReduceInput<T> Input
		{
			get;
			set;
		}

		public Reduce()
		{
		}

		public bool Equals(IReduce<T> other)
		{
			if (other == null) return false;
			return other.Name == Name;
		}

		

	}
}
