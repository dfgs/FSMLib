using FSMLib.Attributes;
using FSMLib.Automatons;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Automatons
{
	public class NonTerminalNode<T> : BaseNode<T>,INonTerminalNode<T>
	{
		public INonTerminalInput<T> Input
		{
			get;
			set;
		}

		IEnumerable<IBaseNode<T>> INonTerminalNode<T>.Nodes
		{
			get { return Nodes; }
		}

		public List<IBaseNode<T>> Nodes
		{
			get;
			set;
		}

		IEnumerable<IRuleAttribute> INonTerminalNode<T>.Attributes
		{
			get => Attributes;
		}

		public List<IRuleAttribute> Attributes
		{
			get;
			set;
		}

		public NonTerminalNode()
		{
			this.Nodes = new List<IBaseNode<T>>();
			Attributes = new List<IRuleAttribute>();
		}
	
		public NonTerminalNode(INonTerminalInput<T> Input)
		{
			this.Nodes = new List<IBaseNode<T>>();
			Attributes = new List<IRuleAttribute>();
			this.Input = Input;
		}
		public override IEnumerable<ITerminalInput<T>> EnumerateInputs()
		{
			if (Nodes == null) return Enumerable.Empty<ITerminalInput<T>>();
			return Nodes.SelectMany((item) => item.EnumerateInputs());
		}
		public override string ToString()
		{
			return Input.ToString();
		}


	}
}
