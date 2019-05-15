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

		Segment<T> BuildSegment( Rule<T> Rule);
		void Connect(IEnumerable<Node<T>> Nodes, IEnumerable<BaseTransition<T>> Transitions);
		Node<T> GetTargetNode(Transition<T> Transition);
		Node<T> CreateNode();
		int GetNodeIndex(Node<T> Node);
	}
}
