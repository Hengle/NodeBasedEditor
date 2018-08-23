using NUnit.Framework;
using System;

namespace VEdit.Core.Tests
{
    [TestFixture]
    [TestOf(typeof(Graph))]
    public class GraphTests
    {
        private readonly Graph _graph = new TestGraph();

        [Test]
        public void AddNode_ValidNode_NodesCountIsOne()
        {
            var graph = new TestGraph();
            graph.AddNode(new TestNode());
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
            Node testNode = new TestNode();
            _graph.AddNode(testNode);

            Assert.Throws<ArgumentException>(() => _graph.AddNode(testNode));
        }
    }
}
