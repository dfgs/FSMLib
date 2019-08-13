﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public interface ITerminalInput<T> : IReduceInput<T>
	{
		T Value
		{
			get;
		}

	}
}
