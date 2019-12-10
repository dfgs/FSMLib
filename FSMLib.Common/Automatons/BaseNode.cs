using FSMLib.Attributes;
using FSMLib.Automatons;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Automatons
{
	public abstract class BaseNode<T>:IBaseNode<T>
	{
		IEnumerable<IRuleAttribute> IBaseNode<T>.Attributes
		{
			get => Attributes;
		}

		public List<IRuleAttribute> Attributes
		{
			get;
			set;
		}

		public BaseNode()
		{
			Attributes = new List<IRuleAttribute>();
		}
		//public abstract IEnumerable<ITerminalInput<T>> EnumerateInputs();
	}
}
