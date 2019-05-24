using System;
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
	public class AnyTerminalSegmentFactory<T> : BaseSegmentFactory<AnyTerminal<T>, T>
	{
		public AnyTerminalSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment(IAutomatonTableFactoryContext<T> Context, AnyTerminal<T> Predicate, IEnumerable<BaseAction<T>> OutActions)
		{
			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutActions == null) throw new ArgumentNullException("OutActions");

			State<T> state;
			ShiftOnTerminal<T> action;
			Segment<T> segment;
			List<ShiftOnTerminal<T>> actions;
			

			state = Context.CreateState();

			actions = new List<ShiftOnTerminal<T>>();
			foreach (T input in Context.GetAlphabet())
			{
				action = new ShiftOnTerminal<T>();
				action.TargetStateIndex = Context.GetStateIndex(state);
				action.Input = new TerminalInput<T>() { Value=input };
				actions.Add(action);
			}

			Context.Connect(state.AsEnumerable(), OutActions);

			segment = new Segment<T>();
			segment.Outputs = state.AsEnumerable(); ;
			segment.Actions = actions;

			return segment;
		}

		
	}
}
