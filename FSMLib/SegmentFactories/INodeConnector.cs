﻿using FSMLib.Graphs;
using FSMLib.Graphs.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.SegmentFactories
{
	public interface INodeConnector<T>
	{
		void Connect(string Rule, IEnumerable<Node<T>> Nodes, IEnumerable<Transition<T>> Transitions);
	}
}
