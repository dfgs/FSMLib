using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Automatons
{
	public class NonTerminalNode<T> : BaseNode<T>
	{
		public string Name
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


	}
}
