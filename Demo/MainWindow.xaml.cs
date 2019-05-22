using FSMLib;
using FSMLib.Graphs;
using FSMLib.Graphs.Transitions;
using FSMLib.Helpers;
using FSMLib.Predicates;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
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

namespace Demo
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ObservableCollection<GraphView> views;
		private GraphFactory<char> graphFactory;
		private CharRuleParser parser;

		private static char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 's', 't' };

		public MainWindow()
		{
			InitializeComponent();

			graphFactory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			parser = new CharRuleParser();

			views = new ObservableCollection<GraphView>();
			tabControl.ItemsSource = views;
			CreateView(parser.Parse("A=a{BCD}e"), parser.Parse("BCD=b{C}d"), parser.Parse("C = c"));
			CreateView(parser.Parse("A=abcde"));

			CreateView(new ZeroOrMore<char>() { Item= new Terminal<char>() {Value='a' }  });
			CreateView(parser.Parse("A=a{S}a"), parser.Parse("S=st"));


			CreateView(parser.Parse("A=a{B}a"), parser.Parse("B=b{A}b"));

			CreateView(parser.Parse("A=a{S}a"), parser.Parse("S={S}b"), parser.Parse("S=c"));



		}
		private void CreateView(BasePredicate<char> Predicate)
		{
			Rule<char> rule;

			rule = new Rule<char>() { Predicate = Predicate };
			CreateView(graphFactory.BuildGraph(rule.AsEnumerable(), alphabet));
		}
		private void CreateView(params Rule<char>[] Rules)
		{
			CreateView(graphFactory.BuildGraph(Rules,alphabet));
		}
		private void CreateView(Graph<char> Model)
		{
			GraphView view;

			view = new GraphView();
			views.Add(view);
			view.SetGraphs(CreateGraph(Model), CreateGraph(graphFactory.BuildDeterministicGraph(Model)) );
		}
		private static Graph CreateGraph<T>(Graph<T> Model)
		{
			Node n;

			Graph graph = new Graph("graph");
			graph.GraphAttr.Orientation = Microsoft.Glee.Drawing.Orientation.Landscape;
			
			foreach (Node<T> node in Model.Nodes)
			{
				n=graph.AddNode(Model.Nodes.IndexOf(node).ToString());

				n.UserData = string.Join(",", node.ReductionTransitions.Select(item=>$"{item.Name}:{item.TargetNodeIndex}"));

				if (node.ReductionTransitions.Count>0) n.Attr.Shape = Microsoft.Glee.Drawing.Shape.DoubleCircle;
				else n.Attr.Shape = Microsoft.Glee.Drawing.Shape.Circle;
			}
			foreach (Node<T> node in Model.Nodes)
			{
				foreach (TerminalTransition<T> transition in node.TerminalTransitions)
				{
					graph.AddEdge(Model.Nodes.IndexOf(node).ToString(), transition.Value.ToString(), transition.TargetNodeIndex.ToString());
				}
				foreach (NonTerminalTransition<T> transition in node.NonTerminalTransitions)
				{
					graph.AddEdge(Model.Nodes.IndexOf(node).ToString(), transition.Name, transition.TargetNodeIndex.ToString());
				}
			}

			return graph;
		}



	}
}
