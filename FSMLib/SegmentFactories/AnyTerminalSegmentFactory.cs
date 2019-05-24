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
	public class AnyTerminalSegmentFactory<T> : BaseSegmentFactory<AnyTerminal<T>, T>
	{
		public AnyTerminalSegmentFactory( ISegmentFactoryProvider<T> SegmentFactoryProvider) : base(SegmentFactoryProvider)
		{
		}

		public override Segment<T> BuildSegment(IActionTableFactoryContext<T> Context, AnyTerminal<T> Predicate, IEnumerable<BaseAction<T>> OutActions)
		{
			if (Context == null) throw new ArgumentNullException("Context");
			if (Predicate == null) throw new ArgumentNullException("Predicate");
			if (OutActions == null) throw new ArgumentNullException("OutActions");

			Node<T> node;
			ShiftOnTerminal<T> action;
			Segment<T> segment;
			List<ShiftOnTerminal<T>> actions;
			

			node = Context.CreateNode();

			actions = new List<ShiftOnTerminal<T>>();
			foreach (T input in Context.GetAlphabet())
			{
				action = new ShiftOnTerminal<T>();
				action.TargetNodeIndex = Context.GetNodeIndex(node);
				action.Value = input ;
				actions.Add(action);
			}

			Context.Connect(node.AsEnumerable(), OutActions);

			segment = new Segment<T>();
			segment.Outputs = node.AsEnumerable(); ;
			segment.Actions = actions;

			return segment;
		}

		
	}
}
