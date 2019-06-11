﻿using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Predicates
{
	public interface ISituationPredicate<T>:IPredicate<T>
	{

		bool Match(T Input);
		bool Match(IInput<T> Input);

	}
}
