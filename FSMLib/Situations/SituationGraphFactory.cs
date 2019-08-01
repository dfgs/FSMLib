﻿using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.Situations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.LexicalAnalysis.Situations
{
	public class SituationGraphFactory<T>: ISituationGraphFactory<T>
	{
		private ISituationGraphSegmentFactory<T> situationGraphSegmentFactory;

		public SituationGraphFactory(ISituationGraphSegmentFactory<T> SituationGraphSegmentFactory)
		{
			if (SituationGraphSegmentFactory == null) throw new ArgumentNullException("SituationGraphSegmentFactory");
			this.situationGraphSegmentFactory = SituationGraphSegmentFactory;
		}
		public SituationGraph<T> BuildSituationGraph(IEnumerable<IRule<T>> Rules)
		{
			SituationNode<T> rootNode;
			SituationGraphSegment<T> segment;
			//Sequence predicate;
			SituationGraph<T> graph;

			if (Rules == null) throw new ArgumentNullException("Rules");

			graph = new SituationGraph<T>();
			
			foreach (IRule<T> rule in Rules)
			{
				/*predicate = new Sequence();
				predicate.Items.Add(rule.Predicate);
				predicate.Items.Add(new Reduce() );*/

				segment = situationGraphSegmentFactory.BuildSegment(graph.Nodes,rule,  rule.Predicate, Enumerable.Empty<SituationEdge<T>>());
				rootNode = CreateNode(graph);
				rootNode.Rule = rule;
				rootNode.Edges.AddRange(segment.InputEdges);
			}

			
			return graph;
		}

		


		private SituationNode<T> CreateNode(SituationGraph<T> Graph)
		{
			SituationNode<T> node;

			node = new SituationNode<T>();
			Graph.Nodes.Add(node);

			return node;
		}





	}
}