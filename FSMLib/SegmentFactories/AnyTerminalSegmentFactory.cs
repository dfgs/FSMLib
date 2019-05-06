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
	public class AnyTerminalSegmentFactory<T> : BaseSegmentFactory<AnyTerminal<T>, T>
	{
		public AnyTerminalSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment(INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, AnyTerminal<T> Predicate, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			Node<T> node;
			Transition<T> transition;
			Segment<T> segment;

			node = NodeContainer.CreateNode();
			transition = new Transition<T>();
			transition.TargetNodeIndex = NodeContainer.GetNodeIndex(node);
			transition.Input = new AnyTerminalInput<T>();

			NodeConnector.Connect(node.AsEnumerable(), OutTransitions);

			segment = new Segment<T>();
			segment.Outputs = node.AsEnumerable(); ;
			segment.Inputs = transition.AsEnumerable();

			return segment;
		}


	}
}
