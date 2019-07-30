﻿
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public class TerminalNode<T>:BaseNode<T>
	{

		public ITerminalInput<T> Input
		{
			get;
			set;
		}

		public TerminalNode()
		{

		}
		public TerminalNode(ITerminalInput<T> Input)
		{
			this.Input = Input;
		}

		public override IEnumerable<ITerminalInput<T>> EnumerateInputs()
		{
			yield return Input;
		}


	}
}
