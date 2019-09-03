using FSMLib.Common;
using FSMLib.Predicates;
using FSMLib.SyntaxisAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FSMLib.SyntaxicAnalysis.Predicates
{
	[Serializable]
	public class Or: SyntaxicPredicate, IOrPredicate<Token>
	{

		[XmlIgnore]
		IEnumerable<IPredicate<Token>> IOrPredicate<Token>.Items => Items;


		[XmlArray]
		public List<SyntaxicPredicate> Items
		{
			get;
			set;
		}


		public Or()
		{
			Items = new List<SyntaxicPredicate>();
		}

		public Or(params SyntaxicPredicate[] Predicates)
		{
			Items = new List<SyntaxicPredicate>();
			Items.AddRange(Predicates);
		}

		public override string ToString()
		{
			return ToString(null);
		}


		public override string ToString(ISituationPredicate<Token> CurrentPredicate)
		{
			if (Items.Count == 1) return Items[0].ToString(CurrentPredicate);
			return $"({string.Join("|", Items.Select(item => item.ToString(CurrentPredicate)))})";

		}

		public override bool Equals(IPredicate<Token> other)
		{
			if (!(other is Or o)) return false;
			if (Items == null) return o.Items == null;
			return ((IOrPredicate<Token>)this).Items.IsStrictelyIndenticalToEx(o.Items);
		}

	}
}
