using FSMLib.Common;
using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.LexicalAnalysis.Predicates
{
	[Serializable]
	public class Or: LexicalPredicate, IOrPredicate<char>
	{

		[XmlIgnore]
		IEnumerable<IPredicate<char>> IOrPredicate<char>.Items => Items;


		[XmlArray]
		public List<LexicalPredicate> Items
		{
			get;
			set;
		}


		public Or()
		{
			Items = new List<LexicalPredicate>();
		}

		public Or(params LexicalPredicate[] Predicates)
		{
			Items = new List<LexicalPredicate>();
			Items.AddRange(Predicates);
		}

		public override string ToString()
		{
			return ToString(null);
		}


		public override string ToString(ISituationPredicate<char> CurrentPredicate)
		{
			if (Items.Count == 1) return Items[0].ToString(CurrentPredicate);
			return $"({string.Join("|", Items.Select(item => item.ToString(CurrentPredicate)))})";

		}

		public override bool Equals(IPredicate<char> other)
		{
			if (!(other is Or o)) return false;
			if (Items == null) return o.Items == null;
			return ((IOrPredicate<char>)this).Items.IsStrictelyIndenticalToEx(o.Items);
		}

	}
}
