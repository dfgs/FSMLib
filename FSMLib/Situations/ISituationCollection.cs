using FSMLib.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationCollection<T>:IEnumerable<Situation<T>>,IEquatable<ISituationCollection<T>>
	{
		Situation<T> this[int Index]
		{
			get;
		}
		int Count
		{
			get;
		}
		void Add(Situation<T> Situation);
		void AddRange(IEnumerable<Situation<T>> Situations);
		bool Contains(Situation<T> Situation);

		IEnumerable<Situation<T>> GetReductionSituations();

		IEnumerable<IInput<T>> GetNextInputs();

		//bool ContainsReductionTo(string Name);
	}
}
