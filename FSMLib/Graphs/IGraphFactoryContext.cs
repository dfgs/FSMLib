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

		IEnumerable<T> GetFirstTerminalsForRule(IEnumerable<Rule<T>> Rules, string Name);
		IEnumerable<T> GetFirstTerminalsAfterTransition(IEnumerable<Rule<T>> Rules, NonTerminalTransition<T> NonTerminalTransition);

		//IEnumerable<Segment<T>> GetDeveloppedSegmentsForRule(IEnumerable<Rule<T>> Rules, string Name);

		IEnumerable<string> GetRuleReductionDependency(IEnumerable<Rule<T>> Rules, string Name);
		IEnumerable<ReductionTransition<T>> GetReductionTransitions(string Name);

	}
}
