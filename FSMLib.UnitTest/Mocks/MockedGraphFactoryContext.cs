using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.UnitTest.Mocks
{
	[ExcludeFromCodeCoverage]
	public class MockedGraphFactoryContext : IGraphFactoryContext<char>
	{
		public Segment<char> BuildSegment(Rule<char> Rule, IEnumerable<BaseTransition<char>> OutTransitions)
		{
			throw new NotImplementedException();
		}

		public void Connect(IEnumerable<Node<char>> Nodes, IEnumerable<BaseTransition<char>> Transitions)
		{
			throw new NotImplementedException();
		}

		public Node<char> CreateNode()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<char> GetAlphabet()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<char> GetFirstTerminalsAfterTransition(IEnumerable<Rule<char>> Rules, NonTerminalTransition<char> NonTerminalTransition)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<char> GetFirstTerminalsForRule(IEnumerable<Rule<char>> Rules, string Name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BaseTransition<char>> GetDeveloppedSegmentsForRule(IEnumerable<Rule<char>> Rules, string Name)
		{
			throw new NotImplementedException();
		}

		public int GetNodeIndex(Node<char> Node)
		{
			throw new NotImplementedException();
		}

		public Node<char> GetTargetNode(int Index)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetRuleReductionDependency(IEnumerable<Rule<char>> Rules, string Name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ReductionTransition<char>> GetReductionTransitions(string Name)
		{
			throw new NotImplementedException();
		}

		/*IEnumerable<Segment<char>> IGraphFactoryContext<char>.GetDeveloppedSegmentsForRule(IEnumerable<Rule<char>> Rules, string Name)
		{
			throw new NotImplementedException();
		}*/
	}
}
