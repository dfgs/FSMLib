using FSMLib.Rules;
using FSMLib.Situations;
using FSMLib.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Common.Table
{
	public class AutomatonTableTuple<T>:IAutomatonTableTuple<T>
	{
		public IState<T> State
		{
			get;
			set;
		}
		public ISituationCollection<T> Situations
		{
			get;
			set;
		}

		public AutomatonTableTuple()
		{

		}
		public AutomatonTableTuple(IState<T> State, ISituationCollection<T> Situations)
		{
			if (State == null) throw new ArgumentNullException();
			if (Situations == null) throw new ArgumentNullException();
			this.State = State;this.Situations = Situations;
		}
	}
}
