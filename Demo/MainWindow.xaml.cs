using FSMLib;
using FSMLib.Graphs;
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
			
		public MainWindow()
		{
			InitializeComponent();

			graphFactory = new GraphFactory<char>(new NodeConnector<char>(), new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			views = new ObservableCollection<GraphView>();
			tabControl.ItemsSource = views;
			CreateView(new TestGraph1());
			CreateView(new TestGraph2());
			CreateView(new TestGraph3());
			CreateView(new TestGraph4());

			CreateView(new ZeroOrMore<char>() { Item= new One<char>() {Value='a' }  });
			//this.gViewer.Graph = CreateGraph<char>(new MockedGraph()); ;
		}
		private void CreateView(RulePredicate<char> Predicate)
		{
			Rule<char> rule;

			rule = new Rule<char>() { Predicate = Predicate };
			CreateView(graphFactory.BuildGraph(rule.AsEnumerable()));
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

			foreach (Node<T> node in Model.Nodes)
			{
				n=graph.AddNode(node.Name);
				if (node.RecognizedRules.Count>0) n.Attr.Shape = Microsoft.Glee.Drawing.Shape.DoubleCircle;
				else n.Attr.Shape = Microsoft.Glee.Drawing.Shape.Circle;
			}
			foreach (Node<T> node in Model.Nodes)
			{
				foreach (Transition<T> transition in node.Transitions)
				{
					graph.AddEdge(node.Name, transition.Input.ToString(), transition.TargetNodeIndex.ToString());
				}
			}

			return graph;
		}



	}
}
