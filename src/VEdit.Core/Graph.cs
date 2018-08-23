using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("VEdit.Core.Tests")]

namespace VEdit.Core
{
    [Serializable]
    public abstract class Graph
    {
        private readonly HashSet<Node> _nodes = new HashSet<Node>();
        public IEnumerable<Node> Nodes => _nodes;

        // TODO: Break node links from old graph
        public void AddNode(Node node)
        {
            if (node is null)
                throw new ArgumentNullException(nameof(node));

            if (!_nodes.Add(node))
                throw new ArgumentException("Node is already present in the collection.");

            node.Graph = this;
        }

        public bool TryAddNode(Node node)
        {
            var result = !(node is null) && _nodes.Add(node);

            if (result)
                node.Graph = this;

            return result;
        }
    }
}
