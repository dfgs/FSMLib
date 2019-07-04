using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	public interface IOptionalPredicate<T> : IPredicate<T>
	{

		IPredicate<T> Item
		{
			get;
		}



	}
}