using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Inputs
{
	public class TerminalRangeInput<T>:BaseTerminalInput<T>, IActionInput<T>
	{
		private static Comparer<T> comparer = Comparer<T>.Default;
		public T FirstValue
		{
			get;
			set;
		}
		public T LastValue
		{
			get;
			set;
		}



		public override bool Equals(IInput<T> other)
		{
			if (other is TerminalRangeInput<T> o)
			{
				return o.FirstValue.Equals(this.FirstValue) && o.LastValue.Equals(this.LastValue);
			}
			return false;
		}




		public override bool Match(T Input)
		{
			if (comparer == null) return false;

			return ((comparer.Compare(Input, FirstValue) >= 0) && (comparer.Compare(Input, LastValue) <= 0));
		}
		public override bool Match(IInput<T> Input)
		{
			if ((Input is TerminalInput<T> terminal)) return Match(terminal.Value);
			else if (Input is TerminalRangeInput<T> terminalRange) return (terminalRange.FirstValue.Equals(this.FirstValue) && terminalRange.LastValue.Equals(this.LastValue));
			else return false;

		}

		public override string ToString()
		{
			return $"[{FirstValue}-{LastValue}]";
		}

		public override int GetHashCode()
		{
			return FirstValue.GetHashCode()*31+LastValue.GetHashCode();
		}

	}
}
