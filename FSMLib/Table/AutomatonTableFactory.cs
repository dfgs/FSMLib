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

namespace FSMLib.Table
{
	public class AutomatonTableFactory<T> : IAutomatonTableFactory<T>
	{

		public AutomatonTableFactory( )
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

				State.ReductionActions.Add(reduce);
			}
		}
	
		private AutomatonTableTuple<T> DevelopSituationsAndCreateTupleIfNotExists(AutomatonTable<T> AutomatonTable, ISituationGraph<T> Graph, Stack<AutomatonTableTuple<T>> OpenList, ISituationDictionary<T> SituationDictionary, IEnumerable<Situation<T>> Situations)
		{
			AutomatonTableTuple<T>  nextTuple;
			State<T> state;
			ISituationCollection<T> developpedSituations;

			developpedSituations = Graph.Develop(Situations);
			nextTuple = SituationDictionary.GetTuple(developpedSituations);
			if (nextTuple == null)
			{
				state = new State<T>();
				AddReductionsToState(state, developpedSituations);
				AutomatonTable.States.Add(state);
				nextTuple = SituationDictionary.CreateTuple(state, developpedSituations);
				OpenList.Push(nextTuple);
			}

			return nextTuple;
		}
		

		public AutomatonTable<T> BuildAutomatonTable(ISituationGraph<T> SituationGraph)
		{

			SituationDictionary<T> situationDictionary;
			IEnumerable<Situation<T>> nextSituations;
			AutomatonTable<T> automatonTable;
			
			AutomatonTableTuple<T> currentTuple,nextTuple;
			Stack<AutomatonTableTuple<T>> openList;
			Shift<T> action;


			if (SituationGraph == null) throw new System.ArgumentNullException("SituationGraph");

			
			automatonTable = new AutomatonTable<T>();
			

			situationDictionary = new SituationDictionary<T>();
			openList = new Stack<AutomatonTableTuple<T>>();

			nextSituations = SituationGraph.CreateAxiomSituations();
			nextTuple = DevelopSituationsAndCreateTupleIfNotExists(automatonTable, SituationGraph, openList, situationDictionary, nextSituations);

			while (openList.Count>0)
			{
				currentTuple = openList.Pop();
				foreach (BaseInput<T> input in currentTuple.Situations.GetNextInputs())
				{
					nextSituations = SituationGraph.CreateNextSituations(currentTuple.Situations, input);
					nextTuple=DevelopSituationsAndCreateTupleIfNotExists(automatonTable, SituationGraph, openList, situationDictionary, nextSituations);

					action = new Shift<T>() { Input = input, TargetStateIndex = automatonTable.States.IndexOf(nextTuple.State) };
					currentTuple.State.ShiftActions.Add(action);
				}

			}

			return automatonTable;
		}









	}

}
