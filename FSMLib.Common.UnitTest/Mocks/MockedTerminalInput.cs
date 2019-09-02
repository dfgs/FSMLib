using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.UnitTest.Mocks
{
	public class MockedTerminalInput : ITerminalInput<char>
	{
		public char Value
		{
			get;
			private set;
		}

		public MockedTerminalInput(char Value)
		{
			this.Value = Value;
		}

		public bool Equals(IInput<char> other)
		{
			return (other is MockedTerminalInput o )&& (o.Value==Value) ;
		}

		public bool Match(IInput<char> Other)
		{
			return (Other is MockedTerminalInput o) && (o.Value == Value);
		}

		public bool Match(char Value)
		{
			return this.Value==Value;
		}
	}
}
