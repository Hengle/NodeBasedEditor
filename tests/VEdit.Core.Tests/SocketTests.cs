using NUnit.Framework;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class SocketTests
    {
        private struct SomeStruct
        {
            public string SomeString;
        }

        [Test]
        public void SplittableSocket_SameAsDataSocket()
        {
            Node node = new GenericNode();
            DataSocket socket = new DataSocket(node, SocketType.Input, typeof(SomeStruct));
            SplittableSocket split = new SplittableSocket(socket);

            Assert.AreSame(split.Node, socket.Node);
            Assert.AreEqual(socket.DataType, split.DataType);
            Assert.AreEqual(socket.Name, split.Name);
            Assert.AreEqual(socket.Type, split.Type);
            Assert.AreEqual(1, split.Children.Count);
            Assert.IsNull(split.Parent);
        }

        [Test]
        public void SplittableSocket_HasChildren()
        {
            Node node = new GenericNode();
            DataSocket socket = new DataSocket(node, SocketType.Input, typeof(SomeStruct));
            SplittableSocket split = new SplittableSocket(socket);

            Assert.AreEqual(1, split.Children.Count);
            Assert.AreEqual(split, split.Children[0].Parent);
            Assert.AreEqual(nameof(SomeStruct.SomeString), split.Children[0].Name);
            Assert.AreEqual(split.Node, split.Children[0].Node);
            Assert.AreEqual(typeof(string), split.Children[0].DataType);
        }
    }
}
