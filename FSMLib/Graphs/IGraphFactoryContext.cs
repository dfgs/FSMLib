using FSMLib.Graphs.Transitions;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public interface IGraphFactoryContext<T>
	{
		/*IEnumerable<Rule<T>> Rules
		{
			get;
		}*/

		Segment<T> BuildSegment( Rule<T> Rule, IEnumerable<BaseTransition<T>> OutTransitions);
		void Connect(IEnumerable<Node<T>> Nodes, IEnumerable<BaseTransition<T>> Transitions);
		Node<T> GetTargetNode(int Index);
		Node<T> CreateNode();
		int GetNodeIndex(Node<T> Node);

		IEnumerable<T> GetAlphabet();
	}
}
