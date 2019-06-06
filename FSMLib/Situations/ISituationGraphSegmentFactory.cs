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

		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, BasePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, SituationPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, Sequence<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, Or<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, Optional<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, ZeroOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, OneOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		

	}
}
