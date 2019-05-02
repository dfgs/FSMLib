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
	public class AnySegmentFactory<T> : BaseSegmentFactory<Any<T>, T>
	{
		public AnySegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment(INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, Any<T> Predicate)
		{
			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			if (Predicate == null) throw new ArgumentNullException("Predicate");

			Segment<T> segment;
			Node<T> node;
			Transition<T> transition;

			segment = new Segment<T>();

			
			node = NodeContainer.CreateNode();
			transition = new Transition<T>();
			transition.TargetNodeIndex = NodeContainer.GetNodeIndex(node);
			transition.Input = new AnyInput<T>();

			segment.Inputs =  transition.AsEnumerable();
			segment.Outputs= node.AsEnumerable();

			return segment;
		}


	}
}
