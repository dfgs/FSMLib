using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public class NonTerminalNode<T> : BaseNode<T>
	{
		public INonTerminalInput<T> Input
		{
			get;
			set;
		}

		public List<BaseNode<T>> Nodes
		{
			get;
			set;
		}

		public NonTerminalNode()
		{
			this.Nodes = new List<BaseNode<T>>();
		}
		public NonTerminalNode(INonTerminalInput<T> Input)
		{
			this.Nodes = new List<BaseNode<T>>();
			this.Input = Input;
		}
		public override IEnumerable<ITerminalInput<T>> EnumerateInputs()
		{
			if (Nodes == null) return Enumerable.Empty<ITerminalInput<T>>();
			return Nodes.SelectMany((item) => item.EnumerateInputs());
		}


	}
}
