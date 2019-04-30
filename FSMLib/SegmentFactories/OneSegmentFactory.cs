using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class OneSegmentFactory<T> : BaseSegmentFactory<One<T>, T>
	{
		public OneSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment(INodeContainer<T> NodeContainer, INodeConnector<T> NodeConnector, One<T> Predicate)
		{
			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			if (Predicate == null) throw new ArgumentNullException("Predicate");

			Segment<T> segment;
			Node<T> node;

			segment = new Segment<T>();

			
			node = NodeContainer.CreateNode();
			segment.Inputs = new Node<T>[] { node };
			segment.Outputs= new Node<T>[] { node };

			return segment;
		}


	}
}
