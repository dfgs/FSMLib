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

		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule,  IPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule, ISituationPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule, ISequencePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule, IOrPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule, IOptionalPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule,  IZeroOrMorePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, IRule<T> Rule, IOneOrMorePredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		

	}
}
