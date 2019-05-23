using FSMLib.Graphs;
using FSMLib.Rules;
using FSMLib.SegmentFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMLib.Helpers
{
	public static class GraphHelper
	{
		public static Graph<char> BuildGraph(IEnumerable<string> Rules, IEnumerable<char> Alphabet)
		{
			GraphFactory<char> graphFactory;
			Graph<char> graph;

			graphFactory = new GraphFactory<char>(new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			graph = graphFactory.BuildGraph(Rules.Select(item => RuleHelper.BuildRule(item)).ToArray(), Alphabet);
		
			return graph;

		}

		public static Graph<char> BuildDeterminiticGraph(IEnumerable<string> Rules,IEnumerable<char> Alphabet)
		{
			GraphFactory<char> graphFactory;
			Graph<char> graph,detGraph;

			graphFactory = new GraphFactory<char>(new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			graph=graphFactory.BuildGraph( Rules.Select(item => RuleHelper.BuildRule(item)).ToArray(), Alphabet );
			detGraph = graphFactory.BuildDeterministicGraph(graph);

			return detGraph;

		}
	}
}
