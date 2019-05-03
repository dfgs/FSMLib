﻿using FSMLib.Graphs;
using FSMLib.Predicates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SegmentFactories
{
	public  interface ISegmentFactory<T>
	{
		Segment<T> BuildSegment(string Rule, INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, RulePredicate<T> Predicate,IEnumerable<Transition<T>> OutTransitions);
	}
	public interface ISegmentFactory<TPredicate,T>: ISegmentFactory<T>
		where TPredicate:RulePredicate<T>
	{
		Segment<T> BuildSegment(string Rule, INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, TPredicate Predicate, IEnumerable<Transition<T>> OutTransitions);
	}
}
