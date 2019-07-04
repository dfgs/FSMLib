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
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, ISituationPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, ISequencePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IOrPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IOptionalPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule,  IZeroOrMorePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IOneOrMorePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		

	}
}
