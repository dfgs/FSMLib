using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FSMLib.Rules;

namespace FSMLib.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class MockedSituationProducer : ISituationProducer<char>
	{
		public IEnumerable<string> GetNextNonTerminals(IEnumerable<Situation<char>> Situations)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Situation<char>> GetNextSituations(IEnumerable<Situation<char>> Situations, char Value)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Situation<char>> GetNextSituations(IEnumerable<Situation<char>> Situations, string Name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<char> GetNextTerminals(IEnumerable<Situation<char>> Situations)
		{
			throw new NotImplementedException();
		}
	}
}
