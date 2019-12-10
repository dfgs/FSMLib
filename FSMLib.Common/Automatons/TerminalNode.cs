
using FSMLib.Automatons;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Automatons
{
	public class TerminalNode<T>:BaseNode<T>,ITerminalNode<T>
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

		public override string ToString()
		{
			return Input.ToString();
		}

	}
}
