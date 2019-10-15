using FSMLib;
using FSMLib.Table;
using FSMLib.Actions;
using FSMLib.Predicates;
using FSMLib.Rules;
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
using FSMLib.LexicalAnalysis.Rules;
using FSMLib.LexicalAnalysis.Predicates;
using FSMLib.LexicalAnalysis.Tables;
using FSMLib.Common.Table;
using FSMLib.Common.Actions;

namespace Demo
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly ObservableCollection<GraphView> views;
		private readonly AutomatonTableFactory<char> lexicalAutomatonTableFactory;
		private readonly AutomatonTableFactory<FSMLib.SyntaxicAnalysis.Token> syntaxicAutomatonTableFactory;

		//private static char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 's', 't', '*' };

		public MainWindow()
		{
			InitializeComponent();

			lexicalAutomatonTableFactory = new AutomatonTableFactory<char>();
			syntaxicAutomatonTableFactory = new AutomatonTableFactory<FSMLib.SyntaxicAnalysis.Token>();
	
			views = new ObservableCollection<GraphView>();
			tabControl.ItemsSource = views;

			CreateLexicalView(@"Symbol*=\.;",
				@"EscapedChar=\\.;",
				@"NonEscapedChar=a;",
				@"Letter*={NonEscapedChar}|{EscapedChar};",
				@"String*={Letter}+;");
	

			CreateSyntaxicView(@"A*=<C,a>;");
				//@"A*=[a-c]e;",
				//@"B*=[b-d]e;");


			CreateLexicalView(@"Symbol *=\[|\]|\{|\}|\.|\+|\*|\?|\||\;|\=|\!|\\;");

			CreateLexicalView(@"Terminal =[a-z]|[A-Z];",
				@"NonAxiomRuleName={Terminal}+;",
				@"AxiomRuleName={Terminal}+\*;",
				@"RuleName*={NonAxiomRuleName}|{AxiomRuleName};");


			CreateLexicalView("A*=ab{C}*;", "C=c;");
			CreateLexicalView("A*=ab{B};", "B={C};", "C={D};", "D=c;");
			CreateLexicalView("A*=a.c;", "B*=abc;");
			
			CreateLexicalView(
				@"A*=[a-c]e;",
				@"B*=[b-d]e;");

			CreateLexicalView(
			@"L=[a-b]|[c-d];",
				@"N={L}+;",
				@"A={L}+\*;",
				@"R*={N}|{A};");

			CreateLexicalView("A*=a{S}a;", "S={S}b;", "S=c;");

			CreateLexicalView("A=abc;", "B=def;","E*={A}|{B};");
			CreateLexicalView("A*=a{B}{C};", "B={C};", "C=b;");
			CreateLexicalView("A*=a{BCD}e;", "BCD=b{C}d;", "C=c;");

			CreateLexicalView("A*=a{B}c;", "B={C};", "C=b;");
			CreateLexicalView("A*=a{B}d;", "B=bc?;");
			CreateLexicalView("A*=a{S}a;", "S={S}b;", "S=c;");

			CreateLexicalView("A*=abcde;");
			CreateLexicalView("A*=a{S}a;", "S=st;");
			CreateLexicalView("A*=a{B}a;", "B=b{A}b;");//*/



		}


		private void CreateLexicalView(params string[] Rules)
		{
			CreateView(lexicalAutomatonTableFactory.BuildAutomatonTable(
				FSMLib.LexicalAnalysis.Helpers.SituationCollectionFactoryHelper.BuildSituationCollectionFactory(Rules.Select(item => FSMLib.LexicalAnalysis.Helpers.RuleHelper.BuildRule(item))), new FSMLib.LexicalAnalysis.Tables.DistinctInputFactory(new FSMLib.LexicalAnalysis.RangeValueProvider()))
				);
		}
		private void CreateSyntaxicView(params string[] Rules)
		{
			CreateView(syntaxicAutomatonTableFactory.BuildAutomatonTable(
				FSMLib.SyntaxicAnalysis.Helpers.SituationCollectionFactoryHelper.BuildSituationCollectionFactory(Rules.Select(item => FSMLib.SyntaxicAnalysis.Helpers.RuleHelper.BuildRule(item))), new FSMLib.SyntaxicAnalysis.Tables.DistinctInputFactory(new FSMLib.SyntaxicAnalysis.RangeValueProvider()))
				);
		}


		private void CreateView<T>(IAutomatonTable<T> Model)
		{
			GraphView view;

			view = new GraphView();
			views.Add(view);
			view.SetAutomatonTables(CreateGraph(Model) );
		}
		private static Graph CreateGraph<T>(IAutomatonTable<T> Automaton)
		{
			Node n;

			Graph graph = new Graph("graph");
			graph.GraphAttr.Orientation = Microsoft.Glee.Drawing.Orientation.Landscape;
			
			foreach (State<T> state in Automaton.States)
			{
				n=graph.AddNode(Automaton.IndexOf(state).ToString());

				n.UserData = string.Join(" ", state.ReduceActions.Select(item=>$"{item.Name}:{item.Input}")); //:{item.TargetStateIndex}:{item.Value}

				if (state.ReduceActions.Count()>0) n.Attr.Shape = Microsoft.Glee.Drawing.Shape.DoubleCircle;
				else n.Attr.Shape = Microsoft.Glee.Drawing.Shape.Circle;

				//if (state.AcceptActions.Count > 0) n.Attr.Color = Microsoft.Glee.Drawing.Color.Blue;
			}
			foreach (State<T> state in Automaton.States)
			{
				foreach (Shift<T> action in state.ShiftActions)
				{
					graph.AddEdge(Automaton.IndexOf(state).ToString(), action.Input.ToString(), action.TargetStateIndex.ToString());
				}
				
			}

			return graph;
		}



	}
}
