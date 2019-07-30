using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Situations
{
	public class SituationGraphFactory: ISituationGraphFactory<char>
	{
		private ISituationGraphSegmentFactory<char> situationGraphSegmentFactory;

		public SituationGraphFactory(ISituationGraphSegmentFactory<char> SituationGraphSegmentFactory)
		{
			if (SituationGraphSegmentFactory == null) throw new ArgumentNullException("SituationGraphSegmentFactory");
			this.situationGraphSegmentFactory = SituationGraphSegmentFactory;
		}
		public SituationGraph<char> BuildSituationGraph(IEnumerable<IRule<char>> Rules)
		{
			SituationNode<char> rootNode;
			SituationGraphSegment<char> segment;
			Sequence predicate;
			SituationGraph<char> graph;

			if (Rules == null) throw new ArgumentNullException("Rules");

			graph = new SituationGraph<char>();
			
			foreach (IRule<char> rule in Rules)
			{
				predicate = new Sequence();
				predicate.Items.Add((LexicalPredicate)rule.Predicate);
				predicate.Items.Add(new Reduce() );

				segment = situationGraphSegmentFactory.BuildSegment(graph.Nodes,rule,  predicate, Enumerable.Empty<SituationEdge<char>>());
				rootNode = CreateNode(graph);
				rootNode.Rule = rule;
				rootNode.Edges.AddRange(segment.InputEdges);
			}

			
			return graph;
		}

		


		private SituationNode<char> CreateNode(SituationGraph<char> Graph)
		{
			SituationNode<char> node;

			node = new SituationNode<char>();
			Graph.Nodes.Add(node);

			return node;
		}





	}
}
