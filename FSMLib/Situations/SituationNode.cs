﻿using FSMLib.Predicates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public class SituationNode<T>
	{
		public BasePredicate<T> Predicate
		{
			get;
			set;
		}

		public List<SituationEdge<T>> Edges
		{
			get;
			set;
		}

		/*public string Reduction
		{
			get;
			set;
		}*/

		public SituationNode()
		{
			Edges = new List<SituationEdge<T>>();
		}

	}
}