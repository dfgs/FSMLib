﻿using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	public interface IOrPredicate<T> : IPredicate<T>
	{

		IEnumerable<IPredicate<T>> Items
		{
			get;
		}



	}
}