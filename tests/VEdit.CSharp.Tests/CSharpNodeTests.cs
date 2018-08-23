using NUnit.Framework;
using VEdit.Core;
using VEdit.Core.Extensions;

namespace VEdit.CSharp.Tests
{
    [TestFixture]
    public class CSharpNodeTests
    {
        [Test]
        public void AddDuplicateParameter_ResultsInUniqueParameter()
        {
            Parameter param = new GenericArgument(TypeConstraints.None);
            Node node = new TestNode();

            GenericSocket socket1 = new GenericSocket(param);
            GenericSocket socket2 = new GenericSocket(param);

            node.AddInput(socket1);
            node.AddOutput(socket2);

            Assert.AreEqual(1, node.Parameters.Count);
        }

        [Test]
        public void RemoveParameter_InexistentParameter_ParametersCountIsZero()
        {
            Parameter param = new GenericArgument(TypeConstraints.None);
            Node node = new TestNode();
            GenericSocket socket1 = new GenericSocket(param);

            node.RemoveInput(socket1);
            Assert.AreEqual(0, node.Parameters.Count);
        }

        [Test]
        public void RemoveParameter_ExistentParameter_ParametersCountIsZero()
        {
            Parameter param = new GenericArgument(TypeConstraints.None);
            Node node = new TestNode();
            GenericSocket socket1 = new GenericSocket(param);

            node.AddInput(socket1);
            Assert.AreEqual(1, node.Parameters.Count);

            node.RemoveInput(socket1);
            Assert.AreEqual(0, node.Parameters.Count);
        }

        [Test]
        public void AddArgument_IsGenericExtension_ReturnsTrue()
        {
            Parameter param = new GenericArgument(TypeConstraints.None);
            Node node = new TestNode();

            GenericSocket socket = new GenericSocket(param);
            node.AddInput(socket);

            Assert.IsTrue(node.IsGeneric());
        }
    }
}
