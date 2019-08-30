using FSMLib.Actions;
using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Inputs;
using FSMLib.Situations;
using FSMLib.Table;
using FSMLib.Common.Situations;
using FSMLib.Common.Actions;

namespace FSMLib.Common.Table
{
	public class AutomatonTableFactory<T> : IAutomatonTableFactory<T>
	{

		public AutomatonTableFactory()
		{
		}

	
		

		private void AddReductionsToState(State<T> State,ISituationCollection<T> Situations)
		{
			Reduce<T> reduce;

			foreach (Situation<T> situation in Situations.GetReductionSituations())
			{
				reduce = new Reduce<T>();
				reduce.Name = situation.Rule.Name;
				reduce.Input = situation.Input;
				reduce.IsAxiom = situation.Rule.IsAxiom;
				State.Add(reduce);
			}
		}
	
		private IAutomatonTableTuple<T> DevelopSituationsAndCreateTupleIfNotExists(IAutomatonTable<T> AutomatonTable, ISituationCollectionFactory<T> SituationCollectionFactory, Stack<IAutomatonTableTuple<T>> OpenList, ISituationDictionary<T> SituationDictionary, IEnumerable<ISituation<T>> Situations)
		{
			IAutomatonTableTuple<T>  nextTuple;
			State<T> state;
			ISituationCollection<T> developpedSituations;

			developpedSituations = SituationCollectionFactory.Develop(Situations);
			nextTuple = SituationDictionary.GetTuple(developpedSituations);
			if (nextTuple == null)
			{
				state = new State<T>();
				AddReductionsToState(state, developpedSituations);
				AutomatonTable.Add(state);
				nextTuple = SituationDictionary.CreateTuple(state, developpedSituations);
				OpenList.Push(nextTuple);
			}

			return nextTuple;
		}
		

		public IAutomatonTable<T> BuildAutomatonTable(ISituationCollectionFactory<T> SituationCollectionFactory, IDistinctInputFactory<T> DistinctInputFactory)
		{

			SituationDictionary<T> situationDictionary;
			IEnumerable<ISituation<T>> nextSituations;
			IAutomatonTable<T> automatonTable;
			IEnumerable<IActionInput<T>> nextInputs;
			IAutomatonTableTuple<T> currentTuple,nextTuple;
			Stack<IAutomatonTableTuple<T>> openList;
			Shift<T> action;
			

			if (SituationCollectionFactory == null) throw new System.ArgumentNullException("SituationCollectionFactory");
			if (DistinctInputFactory == null) throw new ArgumentNullException("DistinctInputFactory");


			automatonTable = new AutomatonTable<T>();
			

			situationDictionary = new SituationDictionary<T>();
			openList = new Stack<IAutomatonTableTuple<T>>();

			nextSituations = SituationCollectionFactory.CreateAxiomSituations();
			nextTuple = DevelopSituationsAndCreateTupleIfNotExists(automatonTable, SituationCollectionFactory, openList, situationDictionary, nextSituations);
			
			while (openList.Count>0)
			{
				currentTuple = openList.Pop();
				nextInputs = DistinctInputFactory.GetDistinctInputs(currentTuple.Situations.SelectMany(item=>item.Predicate.GetInputs()));


				foreach (IActionInput<T> input in nextInputs)
				{
					nextSituations = SituationCollectionFactory.CreateNextSituations(currentTuple.Situations, input);

					nextTuple=DevelopSituationsAndCreateTupleIfNotExists(automatonTable, SituationCollectionFactory, openList, situationDictionary, nextSituations);

					action = new Shift<T>() { Input = input, TargetStateIndex = automatonTable.IndexOf(nextTuple.State) };
					currentTuple.State.Add(action);
				}

			}

			return automatonTable;
		}









	}

}
