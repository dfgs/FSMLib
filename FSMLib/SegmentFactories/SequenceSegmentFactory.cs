using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class SequenceSegmentFactory<T> : BaseSegmentFactory<Sequence<T>, T>
	{
		public SequenceSegmentFactory(INodeContainer NodeContainer, INodeConnector NodeConnector, ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(NodeContainer, NodeConnector, SegmentFactoryProvider)
		{
		}

		public override Segment BuildSegment(Sequence<T> Predicate)
		{
			Segment segment;
			Segment[] segments;
			ISegmentFactory<T> childSegmentFactory;

			if (Predicate == null) throw new ArgumentNullException("Predicate");

			segments = new Segment[Predicate.Items.Count];
			// create segments
			for (int t=0;t<Predicate.Items.Count;t++)
			{
				childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Items[t]);
				segments[t]=childSegmentFactory.BuildSegment(Predicate.Items[t]);
			}
			// connect segments
			for(int t=0;t< Predicate.Items.Count-1; t++)
			{
				NodeConnector.Connect(segments[t].Outputs, segments[t + 1].Inputs);
			}

			segment = new Segment();
			segment.Inputs = segments.First().Inputs;
			segment.Outputs = segments.Last().Outputs;

			return segment;
		}


	}
}
