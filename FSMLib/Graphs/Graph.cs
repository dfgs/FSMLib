using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Graphs
{
	public class Graph<T> 
	{
		public List<Node<T>> Nodes
		{
			get;
			set;
		}

		public List<T> Alphabet
		{
			get;
			set;
		}

		public Graph()
		{
			Nodes = new List<Node<T>>();
			Alphabet = new List<T>();
		}

		
	}
}
