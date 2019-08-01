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

		private List<Shift<T>> shiftActions;
		public IEnumerable<Shift<T>> ShiftActions
		{
			get { return shiftActions; }
		}

		private List<Reduce<T>> reduceActions;
		public IEnumerable<Reduce<T>> ReduceActions
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
			shiftActions = new List<Shift<T>>();
			reduceActions = new List<Reduce<T>>();
		}
		
		public void Add(Shift<T> Action)
		{
			shiftActions.Add(Action) ;
		}

		public void Add(Reduce<T> Action)
		{
			// add reduction conflict based on priority
			reduceActions.Add(Action);
		}

		public Shift<T> GetShift(IActionInput<T> Input)
		{
			Shift<T> action;

			action = shiftActions.FirstOrDefault(item => item.Input.Match(Input));
			return action;
		}

		public Reduce<T> GetReduce(IActionInput<T> Input)
		{
			Reduce<T> action;

			action = reduceActions.FirstOrDefault(item => item.Input.Match(Input));
			return action;
		}



	}
}
