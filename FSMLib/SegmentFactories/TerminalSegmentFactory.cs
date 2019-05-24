﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.Table;
using FSMLib.Actions;
using FSMLib.Predicates;
using FSMLib.Inputs;

namespace FSMLib.SegmentFactories
{
	public class TerminalSegmentFactory<T> : BaseSegmentFactory<Terminal<T>, T>
	{
		public TerminalSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment( IAutomatonTableFactoryContext<T> Context,  Terminal<T> Predicate, IEnumerable<BaseAction<T>> OutActions)
		{
			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutActions == null) throw new ArgumentNullException("OutActions");

			State<T> state;
			ShiftOnTerminal<T> action;
			Segment<T> segment;

			state = Context.CreateState();
			action = new ShiftOnTerminal<T>();
			action.TargetStateIndex = Context.GetStateIndex(state);
			action.Input = new TerminalInput<T>() { Value = Predicate.Value };

			Context.Connect(state.AsEnumerable(), OutActions);

			segment = new Segment<T>();
			segment.Outputs = state.AsEnumerable(); ;
			segment.Actions = action.AsEnumerable();

			return segment;
		}


	}
}
