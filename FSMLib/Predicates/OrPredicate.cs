using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FSMLib.Inputs;
using FSMLib.Rules;

namespace FSMLib.Predicates
{
	[Serializable]
	public abstract class OrPredicate<T>: ExtendedPredicate<T>
	{
	
		[XmlArray]
		public List<BasePredicate<T>> Items
		{
			get;
			set;
		}

		public OrPredicate()
		{
			Items = new List<BasePredicate<T>>();
		}

		
		


		public override string ToString(ISituationPredicate<T> CurrentPredicate)
		{
			if (Items.Count == 1) return Items[0].ToString(CurrentPredicate);
			return $"({string.Join("|", Items.Select(item => item.ToString(CurrentPredicate)))})";
			
		}

		public override bool Equals(IPredicate<T> other)
		{
			if (!(other is OrPredicate<T> o)) return false;
			if (Items == null) return o.Items == null;
			return Items.IsStrictelyIndenticalToEx(o.Items);
		}


		
	}
}
