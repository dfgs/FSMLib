using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Table
{
	public class AutomatonTable<T>:IAutomatonTable<T>
	{
		private List<IState<T>> states;
		public IEnumerable<IState<T>> States
		{
			get { return states; }
		}

		

		public AutomatonTable()
		{
			states = new List<IState<T>>();
		}

		public void Add(IState<T> State)
		{
			states.Add(State);
		}

		public int IndexOf(IState<T> State)
		{
			return states.IndexOf(State);
		}

		public IState<T> GetState(int Index)
		{
			return states[Index];
		}
	}
}
