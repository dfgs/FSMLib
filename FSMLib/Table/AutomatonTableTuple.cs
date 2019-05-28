﻿using FSMLib.Rules;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public class AutomatonTableTuple<T>
	{
		public State<T> State
		{
			get;
			set;
		}
		public IEnumerable<Situation2<T>> Situations
		{
			get;
			set;
		}

		public AutomatonTableTuple()
		{

		}
		public AutomatonTableTuple(State<T> State, IEnumerable<Situation2<T>> Situations)
		{
			if (State == null) throw new ArgumentNullException();
			if (Situations == null) throw new ArgumentNullException();
			this.State = State;this.Situations = Situations;
		}
	}
}
