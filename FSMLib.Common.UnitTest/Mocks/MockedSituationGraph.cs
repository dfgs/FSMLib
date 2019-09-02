using FSMLib.Common.Situations;
using FSMLib.Predicates;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.UnitTest.Mocks
{
	public class MockedSituationGraph : SituationGraph<char>
	{
		public MockedSituationGraph()
		{
			MockedRule rule;
			SituationNode<char> o, a, b, c,e;

			rule = new MockedRule();

			o = new SituationNode<char>();
			a = new SituationNode<char>();
			b = new SituationNode<char>();
			c = new SituationNode<char>();
			e = new SituationNode<char>();

			o.Add(new SituationEdge<char>(rule, new MockedPredicate(), a));
			a.Add(new SituationEdge<char>(rule, new MockedPredicate(), b));
			b.Add(new SituationEdge<char>(rule, new MockedPredicate(), c));
			c.Add(new SituationEdge<char>(rule, new MockedPredicate(), e));

			Add(o);
			Add(a);
			Add(b);
			Add(c);
			Add(e);
		}


	}
}
