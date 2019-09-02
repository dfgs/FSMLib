using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Actions
{
	public interface IReduce<T>:IAction<T>,IEquatable<IReduce<T>>
	{

		string Name
		{
			get;
		}

		bool IsAxiom
		{
			get;
		}

		IReduceInput<T> Input
		{
			get;
		}

		

		

	}
}
