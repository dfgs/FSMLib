using FSMLib;
using FSMLib.Table;
using FSMLib.Actions;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Rules;

using FSMLib.UnitTest.Mocks;
using Microsoft.Glee.Drawing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FSMLib.Situations;

namespace Demo
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ObservableCollection<GraphView> views;
		private AutomatonTableFactory<char> automatonTableFactory;

		private static char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 's', 't' };

		public MainWindow()
		{
			InitializeComponent();

			automatonTableFactory = new AutomatonTableFactory<char>(  );

	
			views = new ObservableCollection<GraphView>();
			tabControl.ItemsSource = views;

			CreateView("A=a{S}a", "S={S}b", "S=c");
			CreateView("A=a{B}{C}", "B={C}", "C=b");
			CreateView("A=ab{B}", "B={C}", "C={D}", "D=c");
			CreateView("A=ab{C}*", "C=c");
			CreateView("A=a{BCD}e", "BCD=b{C}d", "C=c");

			CreateView("A=a{B}c", "B={C}", "C=b");
			CreateView("A=a{B}d", "B=bc?");
			CreateView("A=a{S}a", "S={S}b", "S=c");

			CreateView("A=abcde");
			CreateView("A=a{S}a", "S=st");
			//CreateView("A=a{B}a", "B=b{A}b");//*/



		}
		private void CreateView(BasePredicate<char> Predicate)
		{
			Rule<char> rule;

			rule = new Rule<char>() { Predicate = Predicate };
			CreateView(automatonTableFactory.BuildAutomatonTable(rule.AsEnumerable(), alphabet));
		}
		private void CreateView(params string[] Rules)
		{
			CreateView(automatonTableFactory.BuildAutomatonTable(Rules.Select( item=>RuleHelper.BuildRule(item)) ,alphabet));
		}
		private void CreateView(AutomatonTable<char> Model)
		{
			GraphView view;

			view = new GraphView();
			views.Add(view);
			view.SetAutomatonTables(CreateGraph(Model) );
		}
		private static Graph CreateGraph<T>(AutomatonTable<T> Model)
		{
			Node n;

			Graph graph = new Graph("graph");
			graph.GraphAttr.Orientation = Microsoft.Glee.Drawing.Orientation.Landscape;
			
			foreach (State<T> state in Model.States)
			{
				n=graph.AddNode(Model.States.IndexOf(state).ToString());

				n.UserData = string.Join(" ", state.ReductionActions.Select(item=>$"{item.Name}:{item.Input}")); //:{item.TargetStateIndex}:{item.Value}

				if (state.ReductionActions.Count>0) n.Attr.Shape = Microsoft.Glee.Drawing.Shape.DoubleCircle;
				else n.Attr.Shape = Microsoft.Glee.Drawing.Shape.Circle;

				//if (state.AcceptActions.Count > 0) n.Attr.Color = Microsoft.Glee.Drawing.Color.Blue;
			}
			foreach (State<T> state in Model.States)
			{
				foreach (ShiftOnTerminal<T> action in state.TerminalActions)
				{
					graph.AddEdge(Model.States.IndexOf(state).ToString(), action.Input.ToString(), action.TargetStateIndex.ToString());
				}
				foreach (ShiftOnNonTerminal<T> action in state.NonTerminalActions)
				{
					graph.AddEdge(Model.States.IndexOf(state).ToString(), "{"+action.Name+"}", action.TargetStateIndex.ToString());
				}
			}

			return graph;
		}



	}
}
