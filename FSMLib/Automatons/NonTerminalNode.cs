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

		public List<BaseNode<T>> States
		{
			get;
			set;
		}

		public NonTerminalNode()
		{
			this.States = new List<BaseNode<T>>();
		}

		public override IEnumerable<T> EnumerateTerminals()
		{
			if (States == null) return Enumerable.Empty<T>();
			return States.SelectMany((item) => item.EnumerateTerminals());
		}


	}
}
