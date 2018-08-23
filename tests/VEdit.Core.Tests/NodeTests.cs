using NUnit.Framework;
using System;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void RemoveInputAndOutput_CountIsZero()
        {
            Node node = new TestNode();
            DataSocket socket = new DataSocket<int>();

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
            Node node = new TestNode();

            Assert.Throws<ArgumentNullException>(() => node.RemoveInput(null));
            Assert.Throws<ArgumentNullException>(() => node.RemoveOutput(null));
        }

        [Test]
        public void RemoveInputAndOutput_NonExistingSocket_ReturnsFalse()
        {
            Node node = new TestNode();
            DataSocket socket = new DataSocket<int>();

            var result1 = node.RemoveInput(socket);
            Assert.IsFalse(result1);

            var result2 = node.RemoveOutput(socket);
            Assert.IsFalse(result2);
        }

        [Test]
        public void AddInputAndOutput_NullSocket_ThrowsArgumentNullException()
        {
            Node node = new TestNode();

            Assert.Throws<ArgumentNullException>(() => node.AddInput(null));
            Assert.Throws<ArgumentNullException>(() => node.AddOutput(null));
        }
    }
}
