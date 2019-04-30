using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs.Inputs;

namespace FSMLib.UnitTest.Mocks
{
	public class MockedSituationProducer : ISituationProducer<char>
	{
		public IEnumerable<IInput<char>> GetDistinctInputs(IEnumerable<Situation<char>> Situations)
		{
			throw new NotImplementedException();
		}
	}
}
