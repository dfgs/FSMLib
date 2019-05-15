using FSMLib;
using FSMLib.Graphs;
using FSMLib.Helpers;
using FSMLib.Predicates;
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

		public MainWindow()
		{
			InitializeComponent();

			graphFactory = new GraphFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			parser = new CharRuleParser();

			views = new ObservableCollection<GraphView>();
			tabControl.ItemsSource = views;
			CreateView(new TestGraph1());
			CreateView(new TestGraph2());
			CreateView(new TestGraph3());
			CreateView(new TestGraph4());

			CreateView(new ZeroOrMore<char>() { Item= new Terminal<char>() {Value='a' }  });
			CreateView(parser.Parse("A=a{S}a"), parser.Parse("S=st"));

			CreateView(parser.Parse("A=a{BCD}e"), parser.Parse("BCD={BC}d"), parser.Parse("BC=bc"));

			CreateView(parser.Parse("A=a{B}a"), parser.Parse("B=b{A}b"));

		}
		private void CreateView(BasePredicate<char> Predicate)
		{
			Rule<char> rule;

			rule = new Rule<char>() { Predicate = Predicate };
			CreateView(graphFactory.BuildGraph(rule.AsEnumerable()));
		}
		private void CreateView(params Rule<char>[] Rules)
		{
			CreateView(graphFactory.BuildGraph(Rules));
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

				if (node.RecognizedRules.Count > 0) n.UserData = string.Join(",", node.RecognizedRules);

				if (node.RecognizedRules.Count>0) n.Attr.Shape = Microsoft.Glee.Drawing.Shape.DoubleCircle;
				else n.Attr.Shape = Microsoft.Glee.Drawing.Shape.Circle;
			}
			foreach (Node<T> node in Model.Nodes)
			{
				foreach (Transition<T> transition in node.Transitions)
				{
					graph.AddEdge(Model.Nodes.IndexOf(node).ToString(), transition.Input.ToString(), transition.TargetNodeIndex.ToString());
				}
			}

			return graph;
		}



	}
}
