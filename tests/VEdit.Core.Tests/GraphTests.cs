using NUnit.Framework;
using System;
using VEdit.Core.Nodes;

namespace VEdit.Core.Tests
{
    [TestFixture]
    [TestOf(typeof(Graph))]
    public class GraphTests
    {
        private readonly Graph _graph = new MethodGraph();

        [Test]
        public void AddNode_ValidNode_NodesCountIsOne()
        {
            var graph = new MethodGraph();
            graph.AddNode(new BranchNode());
            Assert.AreEqual(1, graph.Nodes.Count);
        }

        [Test]
        public void AddNode_ParameterIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _graph.AddNode(null));
        }

        [Test]
        public void AddNode_DuplicateNode_ThrowsArgumentException()
        {
            Node testNode = new BranchNode();
            _graph.AddNode(testNode);

            Assert.Throws<ArgumentException>(() => _graph.AddNode(testNode));
        }
    }
}
