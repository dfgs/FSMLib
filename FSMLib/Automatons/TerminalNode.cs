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
		

		public override IEnumerable<BaseTerminalInput<T>> EnumerateInputs()
		{
			yield return Input as BaseTerminalInput<T>;
		}


	}
}
