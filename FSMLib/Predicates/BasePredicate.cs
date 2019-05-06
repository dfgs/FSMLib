using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.Predicates
{
	[Serializable]
	public abstract class BasePredicate<T>
	{ 
		public abstract IEnumerable<BasePredicate<T>> Enumerate();

		public abstract string ToParenthesisString();


	}
}
