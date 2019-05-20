using FSMLib.Predicates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Rules
{
	[System.Serializable]
    public class Rule<T>
    {
		public string Name
		{
			get;
			set;
		}
		public BasePredicate<T> Predicate
		{
			get;
			set;
		}

		public override string ToString()
		{
			return Predicate.ToString();
		}

	}
}
