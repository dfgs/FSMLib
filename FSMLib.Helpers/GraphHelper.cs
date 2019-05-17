using FSMLib.Graphs;
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
		public static Graph<char> BuildDeterminiticGraph(params string[] Rules)
		{
			CharRuleParser parser;
			GraphFactory<char> graphFactory;
			Graph<char> graph,detGraph;

			parser = new CharRuleParser();
			graphFactory = new GraphFactory<char>(new SegmentFactoryProvider<char>(), new SituationProducer<char>());

			graph=graphFactory.BuildGraph(Rules.Select(item => parser.Parse(item)).ToArray() );
			detGraph = graphFactory.BuildDeterministicGraph(graph);

			return detGraph;

		}
	}
}
