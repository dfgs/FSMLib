﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Actions;
using FSMLib.Predicates;
using FSMLib.Table;

namespace FSMLib.Situations
{
	public class SituationDictionary<T> : ISituationDictionary<T>
	{
		private List<AutomatonTableTuple<T>> tupleCollections;

		public SituationDictionary()
		{
			tupleCollections = new List<AutomatonTableTuple<T>>();
		}

		

		public AutomatonTableTuple<T> GetTuple(ISituationCollection<T> Situations)
		{
			return tupleCollections.FirstOrDefault(item => item.Situations.Equals(Situations));
		}

		/*public AutomatonTableTuple<T> GetTuple(Situation<T> Situation)
		{
			return tupleCollections.FirstOrDefault(item => item.Situations.Contains(Situation));
		}*/


		public AutomatonTableTuple<T> CreateTuple(ISituationCollection<T> Situations)
		{
			AutomatonTableTuple<T> tuple;
			IEnumerable<Situation<T>> reduceSituations;
			Reduce<T> reduceAction;

			tuple = new AutomatonTableTuple<T>();
			tuple.State = new State<T>();
			tuple.Situations = Situations;

			reduceSituations = Situations.Where(item => item.Predicate == ReducePredicate<T>.Instance);
			foreach (Situation<T> reduceSituation in reduceSituations)
			{
				reduceAction = new Reduce<T>() { Name = reduceSituation.Rule.Name };
				//reduceAction.Targets;
				tuple.State.ReductionActions.Add(reduceAction );
			}
	
			tupleCollections.Add(tuple);

			return tuple;
		}




	}
}
