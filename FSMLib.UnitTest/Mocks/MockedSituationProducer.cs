using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs.Inputs;

namespace FSMLib.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class MockedSituationProducer : ISituationProducer<char>
	{
		public IEnumerable<IInput<char>> GetNextInputs(IEnumerable<Situation<char>> Situations)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Situation<char>> GetNextSituations(IEnumerable<Situation<char>> Situations, IInput<char> Input)
		{
			throw new NotImplementedException();
		}
	}
}
