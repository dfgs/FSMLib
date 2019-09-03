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
	public class Sequence: SyntaxicPredicate, ISequencePredicate<Token>
	{


		[XmlIgnore]
		IEnumerable<IPredicate<Token>> ISequencePredicate<Token>.Items => Items;


		[XmlArray]
		public List<SyntaxicPredicate> Items
		{
			get;
			set;
		}


		public Sequence()
		{
			Items = new List<SyntaxicPredicate>();
		}
		public Sequence(params SyntaxicPredicate[] Predicates)
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
			return $"({string.Join("", Items.Select(item => item.ToString(CurrentPredicate)))})";

		}

		public override bool Equals(IPredicate<Token> other)
		{
			if (!(other is Sequence o)) return false;
			if (Items == null) return o.Items == null;
			return ((ISequencePredicate<Token>)this).Items.IsStrictelyIndenticalToEx(o.Items);
		}


	}
}
