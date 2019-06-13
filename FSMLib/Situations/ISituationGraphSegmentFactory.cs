using FSMLib.Predicates;
using FSMLib.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Situations
{
	public interface ISituationGraphSegmentFactory<T>
	{

		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule,  IPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule,  SituationPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule,  SequencePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule,  OrPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule,  OptionalPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule,  ZeroOrMorePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule,  OneOrMorePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		

	}
}
