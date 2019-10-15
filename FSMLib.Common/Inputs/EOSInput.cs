using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Inputs
{
	public class EOSInput<T>: IReduceInput<T>
	{
		/*public T Value
		{
			get { return default(T); }
		}*/
		//A changer ne devrait pas être terminal input
		
		public  bool Equals(IInput<T> other)
		{
			return other is EOSInput<T>;
		}
		public  bool Match(IInput<T> Other)
		{
			if (Other == null) return false;
			return Other is EOSInput<T>;
		}

		public  bool Match(T Value)
		{
			return false;
		}

		public override string ToString()
		{
			return "¤";
		}
		

	}
}
