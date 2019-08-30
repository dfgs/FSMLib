using FSMLib.Actions;
using FSMLib.Common.Actions;
using FSMLib.Inputs;
using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Table
{
	public class State<T>:IState<T>
	{

		private List<IShift<T>> shiftActions;
		public IEnumerable<IShift<T>> ShiftActions
		{
			get { return shiftActions; }
		}

		private List<IReduce<T>> reduceActions;
		public IEnumerable<IReduce<T>> ReduceActions
		{
			get { return reduceActions; }
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
			shiftActions = new List<IShift<T>>();
			reduceActions = new List<IReduce<T>>();
		}
		
		public void Add(IShift<T> Action)
		{
			shiftActions.Add(Action) ;
		}

		public void Add(IReduce<T> Action)
		{
			// add reduction conflict based on priority
			reduceActions.Add(Action);
		}

		public IShift<T> GetShift(IActionInput<T> Input)
		{
			IShift<T> action;

			action = shiftActions.FirstOrDefault(item => item.Input.Match(Input));
			return action;
		}

		public IReduce<T> GetReduce(IActionInput<T> Input)
		{
			IReduce<T> action;

			action = reduceActions.FirstOrDefault(item => item.Input.Match(Input));
			return action;
		}



	}
}
