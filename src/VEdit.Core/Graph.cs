using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("VEdit.Core.Tests")]

namespace VEdit.Core
{
    [Serializable]
    public abstract class Graph
    {
        private readonly HashSet<Node> _nodes = new HashSet<Node>();
        public IReadOnlyList<Node> Nodes => _nodes.ToList();

        // TODO: Break node links from old graph
        public void AddNode(Node node)
        {
            if (node is null)
                throw new ArgumentNullException(nameof(node));

            if (!_nodes.Add(node))
                throw new ArgumentException("Node is already present in the collection.");

            node.Graph = this;
        }
    }
}
