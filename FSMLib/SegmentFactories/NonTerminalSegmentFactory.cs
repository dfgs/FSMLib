using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class NonTerminalSegmentFactory<T> : BaseSegmentFactory<NonTerminal<T>, T>
	{
		public NonTerminalSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment( IGraphFactoryContext<T> Context,  NonTerminal<T> Predicate, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			Node<T> node;
			Transition<T> transition;
			Segment<T> segment;
			List<BaseTransition<T>> transitions;

			node = Context.CreateNode();
			Context.Connect(node.AsEnumerable(), OutTransitions);

			transitions = new List<BaseTransition<T>>();

			transition = new Transition<T>();
			transition.TargetNodeIndex = Context.GetNodeIndex(node);
			transition.Input = new NonTerminalInput<T>() {  Name=Predicate.Name };
			transitions.Add(transition);

			

	
			segment = new Segment<T>();
			segment.Outputs = node.AsEnumerable(); ;
			segment.Inputs = transitions;

			return segment;
		}


	}
}
