using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib
{
	public class Situation<T>
	{
		public Rule<T> Rule
		{
			get;
			set;
		}

		public Predicate<T> Predicate
		{
			get;
			set;
		}

	}
}
