using Microsoft.Glee.Drawing;
using Microsoft.Glee.GraphViewerGdi;
using System;
using System.Collections.Generic;
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
	/// Logique d'interaction pour GraphView.xaml
	/// </summary>
	public partial class GraphView : UserControl
	{

		public static readonly DependencyProperty UserDataProperty = DependencyProperty.Register("UserData", typeof(string), typeof(GraphView));
		public string UserData
		{
			get { return (string)GetValue(UserDataProperty); }
			set { SetValue(UserDataProperty, value); }
		}

		public GraphView( )
		{
			InitializeComponent();
		}

		public void SetAutomatonTables(Graph A)
		{
			gViewer1.Graph = A;
			//gViewer2.Graph = B;
		}

		private void GViewer1_SelectionChanged(object sender, EventArgs e)
		{
			UserData = (((GViewer)sender).SelectedObject as Node)?.UserData?.ToString(); ;
		}
	}
}
