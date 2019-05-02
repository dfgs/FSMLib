﻿using FSMLib;
using FSMLib.Graphs;
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
			CreateView(new MockedGraph());
			//this.gViewer.Graph = CreateGraph<char>(new MockedGraph()); ;
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
			Graph graph = new Graph("graph");

			foreach (Node<T> node in Model.Nodes)
			{
				graph.AddNode(node.Name);
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