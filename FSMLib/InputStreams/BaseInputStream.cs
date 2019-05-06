using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.InputStreams
{
	public abstract class BaseInputStream<T> : IInputStream<T>
	{
		public abstract T Read();
	}
}
