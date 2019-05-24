using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSMLib.ActionTables;
using FSMLib.ActionTables.Actions;
using FSMLib.Predicates;

namespace FSMLib.SegmentFactories
{
	public class NonTerminalSegmentFactory<T> : BaseSegmentFactory<NonTerminal<T>, T>
	{
		public NonTerminalSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment( IActionTableFactoryContext<T> Context,  NonTerminal<T> Predicate, IEnumerable<BaseAction<T>> OutActions)
		{
			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutActions == null) throw new ArgumentNullException("OutActions");

			State<T> state;
			ShifOnNonTerminal<T> action;
			Segment<T> segment;
			List<BaseAction<T>> actions;

			state = Context.CreateState();
			Context.Connect(state.AsEnumerable(), OutActions);

			actions = new List<BaseAction<T>>();

			action = new ShifOnNonTerminal<T>();
			action.TargetStateIndex = Context.GetStateIndex(state);
			action.Name = Predicate.Name;
			actions.Add(action);

			

	
			segment = new Segment<T>();
			segment.Outputs = state.AsEnumerable(); ;
			segment.Actions = actions;

			return segment;
		}


	}
}
