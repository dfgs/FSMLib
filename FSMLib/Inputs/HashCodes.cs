﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public static class HashCodes
	{
		public static readonly int AnyTerminal = ".".GetHashCode();
		public static readonly int EOS = "EOSInput".GetHashCode();
	}
}