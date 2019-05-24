using FSMLib.ActionTables;
using FSMLib.ActionTables.Actions;
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
	public class MockedActionTableFactoryContext : IActionTableFactoryContext<char>
	{
		public Segment<char> BuildSegment(Rule<char> Rule, IEnumerable<BaseAction<char>> OutActions)
		{
			throw new NotImplementedException();
		}

		public void Connect(IEnumerable<Node<char>> Nodes, IEnumerable<BaseAction<char>> Actions)
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

		public IEnumerable<char> GetFirstTerminalsAfterAction(IEnumerable<Rule<char>> Rules, ShifOnNonTerminal<char> NonTerminalAction)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<char> GetFirstTerminalsForRule(IEnumerable<Rule<char>> Rules, string Name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BaseAction<char>> GetDeveloppedSegmentsForRule(IEnumerable<Rule<char>> Rules, string Name)
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

		public IEnumerable<Reduce<char>> GetReductionActions(string Name)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<char> GetFirstTerminalsAfterAction(Node<char> Node, string Name)
		{
			throw new NotImplementedException();
		}

		/*IEnumerable<Segment<char>> IActionTableFactoryContext<char>.GetDeveloppedSegmentsForRule(IEnumerable<Rule<char>> Rules, string Name)
		{
			throw new NotImplementedException();
		}*/
	}
}
