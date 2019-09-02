using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.UnitTest.Mocks
{
	public class MockedReduceInput : IReduceInput<char>
	{
		public bool Equals(IInput<char> other)
		{
			return other is MockedReduceInput;
		}

		public bool Match(IInput<char> Other)
		{
			return Other is MockedReduceInput;
		}

		public bool Match(char Value)
		{
			return true;
		}
	}
}
