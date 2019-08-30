using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationCollection<T>:IEnumerable<ISituation<T>>,IEquatable<ISituationCollection<T>>
	{
		ISituation<T> this[int Index]
		{
			get;
		}
		int Count
		{
			get;
		}
		void Add(ISituation<T> Situation);
		void AddRange(IEnumerable<ISituation<T>> Situations);
		bool Contains(ISituation<T> Situation);

		IEnumerable<ISituation<T>> GetReductionSituations();


	}
}
