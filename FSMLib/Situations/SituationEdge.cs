﻿using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public  class SituationEdge<T>
	{
		public InputPredicate<T> NextPredicate
		{
			get;
			set;
		}


	}
}