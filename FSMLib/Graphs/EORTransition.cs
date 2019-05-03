using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class EORTransition<T>:BaseTransition<T>
	{
		public string Rule
		{
			get;
			set;
		}

		public EORTransition()
		{

		}
		
	}
}
