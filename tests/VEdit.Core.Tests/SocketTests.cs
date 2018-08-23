using NUnit.Framework;
using VEdit.Core.Extensions;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class SocketTests
    {
        [Test]
        public void SplitSocket_SameAsDataSocket()
        {
            Node node = new TestNode();
            DataSocket socket = new DataSocket(typeof(StructWithTwoFields));
            SplitSocket split = new SplitSocket(socket);

            Assert.AreSame(split.Node, socket.Node);
            Assert.AreEqual(socket.DataType, split.DataType);
            Assert.AreEqual(socket.Name, split.Name);
            Assert.AreEqual(2, split.Children.Count);
            Assert.IsFalse(split.HasParent());

            split.Name = "Test";
            Assert.AreEqual(socket.Name, split.Name);
        }

        [Test]
        public void SplitSocket_HasChildren()
        {
            Node node = new TestNode();
            DataSocket socket = new DataSocket(typeof(StructWithTwoFields));
            SplitSocket split = new SplitSocket(socket);

            Assert.AreEqual(2, split.Children.Count);
            Assert.AreEqual(split, split.Children[0].Parent);
            Assert.AreEqual(nameof(StructWithTwoFields.SomeString), split.Children[0].Name);
            Assert.AreEqual(split.Node, split.Children[0].Node);
            Assert.AreEqual(typeof(string), split.Children[0].DataType);
        }
    }
}
