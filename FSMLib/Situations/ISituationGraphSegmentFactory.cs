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

		ISituationGraphSegment<T> BuildSegment(ISituationGraph<T> Graph, IRule<T> Rule,  IPredicate<T> Predicate, IEnumerable<ISituationEdge<T>> Edges);
		ISituationGraphSegment<T> BuildSegment(ISituationGraph<T> Graph, IRule<T> Rule, ISituationPredicate<T> Predicate, IEnumerable<ISituationEdge<T>> Edges);
		ISituationGraphSegment<T> BuildSegment(ISituationGraph<T> Graph, IRule<T> Rule, ISequencePredicate<T> Predicate, IEnumerable<ISituationEdge<T>> Edges);
		ISituationGraphSegment<T> BuildSegment(ISituationGraph<T> Graph, IRule<T> Rule, IOrPredicate<T> Predicate, IEnumerable<ISituationEdge<T>> Edges);
		ISituationGraphSegment<T> BuildSegment(ISituationGraph<T> Graph, IRule<T> Rule, IOptionalPredicate<T> Predicate, IEnumerable<ISituationEdge<T>> Edges);
		ISituationGraphSegment<T> BuildSegment(ISituationGraph<T> Graph, IRule<T> Rule,  IZeroOrMorePredicate<T> Predicate, IEnumerable<ISituationEdge<T>> Edges);
		ISituationGraphSegment<T> BuildSegment(ISituationGraph<T> Graph, IRule<T> Rule, IOneOrMorePredicate<T> Predicate, IEnumerable<ISituationEdge<T>> Edges);
		

	}
}
