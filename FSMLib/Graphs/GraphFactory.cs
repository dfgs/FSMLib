using FSMLib.Graphs.Transitions;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class GraphFactory<T> : IGraphFactory<T>
	{
		private ISegmentFactoryProvider<T> segmentFactoryProvider;
		private ISituationProducer<T> situationProducer;

		public GraphFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider, ISituationProducer<T> SituationProducer)
		{
			if (SegmentFactoryProvider == null) throw new ArgumentNullException("SegmentFactoryProvider");
			this.segmentFactoryProvider = SegmentFactoryProvider;
			if (SituationProducer == null) throw new ArgumentNullException("SituationProducer");
			this.situationProducer = SituationProducer;
		}

		private void AddNonTerminalTransitionToNode(Node<T> node,IEnumerable<NonTerminalTransition<T>> Transitions, GraphFactoryContext<T> context, IEnumerable<Rule<T>> Rules,Rule<T> Axiom)
		{
			Segment<T> nonTerminalSegment;
			ReductionTransition<T> reductionTransition;

			foreach (string name in Transitions.Select(item => item.Name))
			{
				foreach (Rule<T> rule in Rules.Where(item => item.Name == name))
				{
					// Get segment corresponding to non terminal
					nonTerminalSegment = context.BuildSegment(rule,Enumerable.Empty<BaseTransition<T>>() );

					// add reduction to non terminal segment
					foreach (Node<T> outputNode in nonTerminalSegment.Outputs)
					{
						reductionTransition = new ReductionTransition<T>();
						reductionTransition.Name = rule.Name;
						reductionTransition.TargetNodeIndex = context.GetNodeIndex(node);
						//reductionTransition.Value = terminalTransition.Value;
						outputNode.ReductionTransitions.Add(reductionTransition);
					}

					// connect node to non terminal segment
					context.Connect(node.AsEnumerable(), nonTerminalSegment.Inputs);

					

					// we must connect recusively node to all segments, everytime a Non Terminal transition appears
					// but we must exclude Non Terminal transition with current input Name in order to avoid infinite loop
					AddNonTerminalTransitionToNode(node, nonTerminalSegment.Inputs.OfType<NonTerminalTransition<T>>().Where( item=> item.Name!=name ) , context, Rules,Axiom);
				}
			}
		}

		public Graph<T> BuildGraph(IEnumerable<Rule<T>> Rules, IEnumerable<T> Alphabet)
		{
			Graph<T> graph;
			Node<T> root;
			Segment<T> segment;
			GraphFactoryContext<T> context;
			Rule<T> axiom;
	
			if (Rules == null) throw new System.ArgumentNullException("Rules");
			if (Alphabet == null) throw new System.ArgumentNullException("Alphabet");

	
			graph = new Graph<T>();
			graph.Alphabet.AddRange(Alphabet);

			if (!Rules.Any()) return graph;

			axiom = Rules.First();
			
			context = new GraphFactoryContext<T>(segmentFactoryProvider, graph);
			root = context.CreateNode();
			
			// build all segments from rules
			foreach(Rule<T> rule in Rules)
			{
				segment = context.BuildSegment( rule, Enumerable.Empty<BaseTransition<T>>());
				context.Connect(root.AsEnumerable(), segment.Inputs);
			}

			// add accept transition to root segment
			segment = context.BuildSegment(axiom, Enumerable.Empty<BaseTransition<T>>());
			foreach (Node<T> node in segment.Outputs)
			{
				node.AcceptTransitions.Add(new AcceptTransition<T>());
				node.ReductionTransitions.Add(new ReductionTransition<T>() { IsAxiom=true, TargetNodeIndex=0, Name=axiom.Name } );
			}

			// add reduction transition to nodes
			foreach (Node<T> node in graph.Nodes)
			{
				if (node == root) continue;
				AddNonTerminalTransitionToNode(node,node.NonTerminalTransitions.ToArray(),context,Rules,axiom);
			}

			return graph;
		}


		private GraphTuple<T> GetNextTuple(GraphFactoryContext<T> Context, Stack<GraphTuple<T>> OpenList,List<GraphTuple<T>> SituationMapping, IEnumerable<Situation<T>> NextSituations)
		{
			GraphTuple<T> nextTuple;

			nextTuple = SituationMapping.FirstOrDefault(item => item.Situations.IsIndenticalToEx(NextSituations));
			if (nextTuple == null)
			{
				nextTuple = new GraphTuple<T>();
				nextTuple.Node = Context.CreateNode();
				nextTuple.Situations = NextSituations;

				nextTuple.Node.ReductionTransitions.AddRange(NextSituations.SelectMany(item => item.Graph.Nodes[item.NodeIndex].ReductionTransitions));
				nextTuple.Node.AcceptTransitions.AddRange(NextSituations.SelectMany(item => item.Graph.Nodes[item.NodeIndex].AcceptTransitions));

				SituationMapping.Add(nextTuple);
				OpenList.Push(nextTuple);
			}

			return nextTuple;
		}
		public Graph<T> BuildDeterministicGraph(Graph<T> BaseGraph)
		{
			IEnumerable<Situation<T>> nextSituations;
			Graph<T> graph;
			List<GraphTuple<T>> situationMapping;
			GraphTuple<T> currentTuple,nextTuple;
			Stack<GraphTuple<T>> openList;
			ShiftTransition<T> transition;
			GraphFactoryContext<T> context;

			if (BaseGraph == null) throw new System.ArgumentNullException("BaseGraph");

			graph = new Graph<T>();
			graph.Alphabet.AddRange(BaseGraph.Alphabet);

			if (BaseGraph.Nodes.Count == 0) return graph;
			context = new GraphFactoryContext<T>(segmentFactoryProvider,graph);

			situationMapping = new List<GraphTuple<T>>();
			openList = new Stack<GraphTuple<T>>();

			currentTuple= GetNextTuple(context, openList, situationMapping, new Situation<T>() { Graph = BaseGraph, NodeIndex = 0 }.AsEnumerable());
	
			while (openList.Count>0)
			{
				currentTuple = openList.Pop();
				foreach (T value in situationProducer.GetNextTerminals(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(currentTuple.Situations, value);
					// do we have the same situation list in graph ?
					nextTuple = GetNextTuple(context,openList,situationMapping,nextSituations);
					// if not we push this situation list in processing stack
					transition = new TerminalTransition<T>() { Value = value, TargetNodeIndex = context.GetNodeIndex(nextTuple.Node) };
					context.Connect(currentTuple.Node.AsEnumerable(), transition.AsEnumerable());
				}
				foreach (string name in situationProducer.GetNextNonTerminals(currentTuple.Situations))
				{
					nextSituations = situationProducer.GetNextSituations(currentTuple.Situations, name);
					// do we have the same situation list in graph ?
					nextTuple = GetNextTuple(context, openList, situationMapping, nextSituations);
					// if not we push this situation list in processing stack
					transition = new NonTerminalTransition<T>() { Name = name, TargetNodeIndex = context.GetNodeIndex(nextTuple.Node) };
					context.Connect(currentTuple.Node.AsEnumerable(), transition.AsEnumerable());
				}

			}

			// translate reduction transitions, because indices in base graph are not the same in det graph
			foreach (ReductionTransition<T> reductionTransition in graph.Nodes.SelectMany(item=>item.ReductionTransitions))
			{
				nextTuple = situationMapping.FirstOrDefault(item => item.Situations.FirstOrDefault(situation=>situation.NodeIndex==reductionTransition.TargetNodeIndex)!=null );
				if (nextTuple == null) throw new Exception("Failed to translate reduction transitions");
				reductionTransition.TargetNodeIndex = context.GetNodeIndex(nextTuple.Node);
			}

			return graph;
		}









	}

}
