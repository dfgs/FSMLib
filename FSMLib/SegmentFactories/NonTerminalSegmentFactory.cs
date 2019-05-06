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

		public override Segment<T> BuildSegment( INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, NonTerminal<T> Predicate, IEnumerable<BaseTransition<T>> OutTransitions)
		{
			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutTransitions == null) throw new ArgumentNullException("OutTransitions");

			Node<T> node;
			Transition<T> transition;
			Segment<T> segment;

			throw new NotImplementedException();

			node = NodeContainer.CreateNode();
			transition = new Transition<T>();
			transition.TargetNodeIndex = NodeContainer.GetNodeIndex(node);
			transition.Input = new NonTerminalInput<T>() {  Name=Predicate.Name };

			NodeConnector.Connect(node.AsEnumerable(), OutTransitions);

			segment = new Segment<T>();
			segment.Outputs = node.AsEnumerable(); ;
			segment.Inputs = transition.AsEnumerable();

			return segment;
		}


	}
}
