﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public interface IInput<T>:IEquatable<IInput<T>>
	{
		bool Match(IInput<T> Other);
		bool Match(T Value);
	}
}
