﻿using FSMLib;
using FSMLib.ActionTables;
using FSMLib.ActionTables.Actions;
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
		private ActionTableFactory<char> actionTableFactory;

		private static char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 's', 't' };

		public MainWindow()
		{
			InitializeComponent();

			actionTableFactory = new ActionTableFactory<char>( new SegmentFactoryProvider<char>(), new SituationProducer<char>());

	
			views = new ObservableCollection<GraphView>();
			tabControl.ItemsSource = views;

			CreateView("A=a{S}a", "S={S}b", "S=c");
			CreateView("A=a{BCD}e", "BCD=b{C}d", "C=c");

			CreateView("A=a{B}c", "B={C}", "C=b");
			CreateView("A=a{B}d", "B=bc?");
			CreateView("A=a{S}a", "S={S}b", "S=c");

			CreateView("A=abcde");

			CreateView(new ZeroOrMore<char>() { Item= new Terminal<char>() {Value='a' }  });
			CreateView("A=a{S}a", "S=st");


			CreateView("A=a{B}a", "B=b{A}b");



		}
		private void CreateView(BasePredicate<char> Predicate)
		{
			Rule<char> rule;

			rule = new Rule<char>() { Predicate = Predicate };
			CreateView(actionTableFactory.BuildActionTable(rule.AsEnumerable(), alphabet));
		}
		private void CreateView(params string[] Rules)
		{
			CreateView(actionTableFactory.BuildActionTable(Rules.Select( item=>RuleHelper.BuildRule(item)) ,alphabet));
		}
		private void CreateView(ActionTable<char> Model)
		{
			GraphView view;

			view = new GraphView();
			views.Add(view);
			view.SetActionTables(CreateGraph(Model), CreateGraph(actionTableFactory.BuildDeterministicActionTable(Model)) );
		}
		private static Graph CreateGraph<T>(ActionTable<T> Model)
		{
			Node n;

			Graph graph = new Graph("graph");
			graph.GraphAttr.Orientation = Microsoft.Glee.Drawing.Orientation.Landscape;
			
			foreach (Node<T> node in Model.Nodes)
			{
				n=graph.AddNode(Model.Nodes.IndexOf(node).ToString());

				n.UserData = string.Join("/", node.ReductionActions.Select(item=>$"{item.Name} {string.Join(",",item.Targets.Select(target=> target.TargetNodeIndex+"/"+target.Value))}")); //:{item.TargetNodeIndex}:{item.Value}

				if (node.ReductionActions.Count>0) n.Attr.Shape = Microsoft.Glee.Drawing.Shape.DoubleCircle;
				else n.Attr.Shape = Microsoft.Glee.Drawing.Shape.Circle;
			}
			foreach (Node<T> node in Model.Nodes)
			{
				foreach (ShiftOnTerminal<T> action in node.TerminalActions)
				{
					graph.AddEdge(Model.Nodes.IndexOf(node).ToString(), action.Value.ToString(), action.TargetNodeIndex.ToString());
				}
				foreach (ShifOnNonTerminal<T> action in node.NonTerminalActions)
				{
					graph.AddEdge(Model.Nodes.IndexOf(node).ToString(), "{"+action.Name+"}", action.TargetNodeIndex.ToString());
				}
			}

			return graph;
		}



	}
}
