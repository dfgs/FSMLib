using FSMLib.Actions;
using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public class State<T>
	{

		private Dictionary<int,Shift<T>> shiftActions;
		public IEnumerable<Shift<T>> ShiftActions
		{
			get { return shiftActions.Values; }
		}

		private Dictionary<int,Reduce<T>> reduceActions;
		public IEnumerable<Reduce<T>> ReduceActions
		{
			get { return reduceActions.Values; }
		}

		public int ShiftActionCount
		{
			get { return shiftActions.Count; }
		}

		public int ReduceActionCount
		{
			get { return reduceActions.Count; }
		}

		public State()
		{
			shiftActions = new Dictionary<int, Shift<T>>();
			reduceActions = new Dictionary<int, Reduce<T>>();
		}
		
		public void Add(Shift<T> Action)
		{
			shiftActions.Add(Action.GetHashCode(), Action) ;
		}

		public void Add(Reduce<T> Action)
		{
			// check reduction conflict
			if (reduceActions.ContainsKey(Action.GetHashCode())) return;

			reduceActions.Add(Action.GetHashCode(), Action);
		}

		public Shift<T> GetShift(IInput<T> Input)
		{
			Shift<T> action;
			shiftActions.TryGetValue(Input.GetHashCode(), out action);
			return action;
		}

		public Reduce<T> GetReduce(IInput<T> Input)
		{
			Reduce<T> action;
			reduceActions.TryGetValue(Input.GetHashCode(), out action);
			return action;
		}



	}
}
