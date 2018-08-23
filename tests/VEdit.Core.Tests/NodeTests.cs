using NUnit.Framework;
using System;
using VEdit.Core.Extensions;
using VEdit.Core.Nodes;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void AddDuplicateParameter_ResultsInUniqueParameter()
        {
            Parameter param = new GenericArgument(TypeConstraints.None);

            Node node = new MethodNode();
            node.AddParameter(param);
            node.AddParameter(param);

            Assert.AreEqual(1, node.Parameters.Count);
        }

        [Test]
        public void AddArgument_IsGenericExtension_ReturnsTrue()
        {
            Parameter param = new GenericArgument(TypeConstraints.None);

            Node node = new MethodNode();
            node.AddParameter(param);

            Assert.IsTrue(node.IsGeneric());
        }

        [Test]
        public void RemoveInputAndOutput_CountIsZero()
        {
            Node node = new MethodNode();
            DataSocket socket = new DataSocket<int>(node);

            node.AddInput(socket);
            Assert.AreEqual(1, node.Input.Count);

            node.RemoveInput(socket);
            Assert.AreEqual(0, node.Input.Count);

            node.AddOutput(socket);
            Assert.AreEqual(1, node.Output.Count);

            node.RemoveOutput(socket);
            Assert.AreEqual(0, node.Output.Count);
        }

        [Test]
        public void RemoveInputAndOutput_NullSocket_ThrowArgumentNullException()
        {
            Node node = new MethodNode();

            Assert.Throws<ArgumentNullException>(() => node.RemoveInput(null));
            Assert.Throws<ArgumentNullException>(() => node.RemoveOutput(null));
        }

        [Test]
        public void RemoveInputAndOutput_NonExistingSocket_ReturnsFalse()
        {
            Node node = new MethodNode();
            DataSocket socket = new DataSocket<int>(node);

            var result1 = node.RemoveInput(socket);
            Assert.IsFalse(result1);

            var result2 = node.RemoveOutput(socket);
            Assert.IsFalse(result2);
        }

        [Test]
        public void AddInputAndOutput_NullSocket_ThrowsArgumentNullException()
        {
            Node node = new MethodNode();

            Assert.Throws<ArgumentNullException>(() => node.AddInput(null));
            Assert.Throws<ArgumentNullException>(() => node.AddOutput(null));
        }
    }
}
