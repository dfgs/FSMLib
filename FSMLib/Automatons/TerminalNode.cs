using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Tables
{
	public class TerminalNode<T>:BaseNode<T>
	{
		public T Value
		{
			get;
			set;
		}

		public override IEnumerable<T> EnumerateTerminals()
		{
			yield return Value;
		}


	}
}
