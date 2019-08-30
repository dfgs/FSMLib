using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Actions;
using FSMLib.Common.Table;
using FSMLib.Predicates;
using FSMLib.Situations;
using FSMLib.Table;

namespace FSMLib.Common.Situations
{
	public class SituationDictionary<T> : ISituationDictionary<T>
	{
		private List<AutomatonTableTuple<T>> tupleCollections;

		public SituationDictionary()
		{
			tupleCollections = new List<AutomatonTableTuple<T>>();
		}

		

		public IAutomatonTableTuple<T> GetTuple(ISituationCollection<T> Situations)
		{
			return tupleCollections.FirstOrDefault(item => item.Situations.Equals(Situations));
		}

		


		public IAutomatonTableTuple<T> CreateTuple(IState<T> State, ISituationCollection<T> Situations)
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

		


		public IEnumerator<IAutomatonTableTuple<T>> GetEnumerator()
		{
			return tupleCollections.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return tupleCollections.GetEnumerator();
		}
	}
}
