using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Actions;
using FSMLib.Predicates;
using FSMLib.Table;

namespace FSMLib.Situations
{
	public class SituationDictionary<T> : ISituationDictionary<T>,IEnumerable<AutomatonTableTuple<T>>
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

		


		public AutomatonTableTuple<T> CreateTuple(State<T> State, ISituationCollection<T> Situations)
		{
			AutomatonTableTuple<T> tuple;
			//IEnumerable<Situation<T>> reduceSituations;
			//Reduce<T> reduceAction;

			tuple = new AutomatonTableTuple<T>();
			tuple.State = State;
			tuple.Situations = Situations;

			
	
			tupleCollections.Add(tuple);

			return tuple;
		}

		


		public IEnumerator<AutomatonTableTuple<T>> GetEnumerator()
		{
			return tupleCollections.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return tupleCollections.GetEnumerator();
		}
	}
}
