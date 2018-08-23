using NUnit.Framework;
using VEdit.Core.Nodes;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void Node_AddDuplicateParameter_ResultsInUniqueParameter()
        {
            Parameter param = new GenericArgument(TypeConstraints.None);

            Node node = new MethodNode();
            node.AddParameter(param);
            node.AddParameter(param);

            Assert.AreEqual(1, node.Parameters.Count);
        }
    }
}
