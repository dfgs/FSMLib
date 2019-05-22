using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class AnyTerminalSegmentFactory<T> : BaseSegmentFactory<AnyTerminal<T>, T>
	{
		public AnyTerminalSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment(IGraphFactoryContext<T> Context, AnyTerminal<T> Predicate, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			Node<T> node;
			TerminalTransition<T> transition;
			Segment<T> segment;
			List<TerminalTransition<T>> transitions;
			

			node = Context.CreateNode();

			transitions = new List<TerminalTransition<T>>();
			foreach (T input in Context.GetAlphabet())
			{
				transition = new TerminalTransition<T>();
				transition.TargetNodeIndex = Context.GetNodeIndex(node);
				transition.Value = input ;
				transitions.Add(transition);
			}

			Context.Connect(node.AsEnumerable(), OutTransitions);

			segment = new Segment<T>();
			segment.Outputs = node.AsEnumerable(); ;
			segment.Inputs = transitions;

			return segment;
		}

		
	}
}
