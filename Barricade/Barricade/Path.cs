using System.Collections.Generic;

namespace AStarAlgorithm
{
    internal class Path
    {
        private readonly List<Node> nodes;

        public Path(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public List<Node> Nodes
        {
            get { return nodes; }
        }
    }
}