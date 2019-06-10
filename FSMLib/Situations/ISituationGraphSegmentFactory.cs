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

		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, IPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, SituationPredicate<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, Sequence<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, Or<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, Optional<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, ZeroOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		SituationGraphSegment<T> BuildSegment(List<SituationNode<T>> Nodes, Rule<T> Rule, IEnumerable<T> Alphabet, OneOrMore<T> Predicate, IEnumerable<SituationEdge<T>> Edges);
		

	}
}
