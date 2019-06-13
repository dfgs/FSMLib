using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Tables
{
	public class TerminalNode<T>:BaseNode<T>
	{

		public TerminalInput<T> Input
		{
			get;
			set;
		}
		public override IEnumerable<TerminalInput<T>> EnumerateInputs()
		{
			yield return Input;
		}


	}
}
