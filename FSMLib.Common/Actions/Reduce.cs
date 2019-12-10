using FSMLib.Actions;
using FSMLib.Attributes;
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
		}

		public bool IsAxiom
		{
			get;
		}

		public IReduceInput<T> Input
		{
			get;
		}

		public IEnumerable<IRuleAttribute> Attributes
		{
			get ;
		}
		

		public Reduce(string Name,bool IsAxiom, IReduceInput<T> Input, IEnumerable<IRuleAttribute> Attributes)
		{
			if (Name == null) throw new ArgumentNullException("Name");
			if (Input == null) throw new ArgumentNullException("Input");
			this.Name = Name;
			this.IsAxiom = IsAxiom;
			this.Input = Input;
			this.Attributes = Attributes;
		}

		public bool Equals(IReduce<T> other)
		{
			if (other == null) return false;
			return other.Name == Name;
		}

		

	}
}
