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
			CharRuleParser parser;
			GraphFactory<char> graphFactory;
			Graph<char> graph;

			parser = new CharRuleParser();
			graphFactory = new GraphFactory<char>(new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			graph = graphFactory.BuildGraph(Rules.Select(item => parser.Parse(item)).ToArray(), Alphabet);
		
			return graph;

		}

		public static Graph<char> BuildDeterminiticGraph(IEnumerable<string> Rules,IEnumerable<char> Alphabet)
		{
			CharRuleParser parser;
			GraphFactory<char> graphFactory;
			Graph<char> graph,detGraph;

			parser = new CharRuleParser();
			graphFactory = new GraphFactory<char>(new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			graph=graphFactory.BuildGraph( Rules.Select(item => parser.Parse(item)).ToArray(), Alphabet );
			detGraph = graphFactory.BuildDeterministicGraph(graph);

			return detGraph;

		}
	}
}
