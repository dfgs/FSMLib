﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Graphs;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class OrSegmentFactory<T> : BaseSegmentFactory<Or<T>, T>
	{
		public OrSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base( SegmentFactoryProvider)
		{
		}

		public override Segment BuildSegment(INodeContainer NodeContainer, INodeConnector NodeConnector, Or<T> Predicate)
		{
			Segment segment;
			Segment[] segments;
			ISegmentFactory<T> childSegmentFactory;

			if (NodeContainer == null) throw new ArgumentNullException("NodeContainer");
			if (NodeConnector == null) throw new ArgumentNullException("NodeConnector");
			if (Predicate == null) throw new ArgumentNullException("Predicate");

			segments = new Segment[Predicate.Items.Count];
			// create segments
			for (int t = 0; t < Predicate.Items.Count; t++)
			{
				childSegmentFactory = SegmentFactoryProvider.GetSegmentFactory(Predicate.Items[t]);
				segments[t] = childSegmentFactory.BuildSegment(NodeContainer,NodeConnector, Predicate.Items[t]);
			}
			

			segment = new Segment();
			segment.Inputs = segments.SelectMany(item=>item.Inputs);
			segment.Outputs = segments.SelectMany(item => item.Outputs);

			return segment;
		}
	}
}
