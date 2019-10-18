using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis
{
	public static class CharExtenionMethod
	{
		public static char Previous(this char Value)
		{
			if (Value==char.MinValue) throw new ArgumentOutOfRangeException("Value");
			return (char)(Value - 1);
		}
		public static char Next(this char Value)
		{
			if (Value==char.MaxValue) throw new ArgumentOutOfRangeException("Value");
			return (char)(Value + 1);
		}

	}
}
