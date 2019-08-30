using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Table
{
	public interface IAutomatonTable<T> 
	{
		IEnumerable<IState<T>> States
		{
			get;
		}

		void Add(IState<T> State);

		int IndexOf(IState<T> State);

		IState<T> GetState(int Index);



	}
}
