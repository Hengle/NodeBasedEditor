using NUnit.Framework;
using VEdit.Core.Nodes;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class SocketTests
    {
        [Test]
        public void SplitSocket_SameAsDataSocket()
        {
            Node node = new MethodNode();
            DataSocket socket = new DataSocket(node, typeof(StructWithTwoFields));
            SplitSocket split = new SplitSocket(socket);

            Assert.AreSame(split.Node, socket.Node);
            Assert.AreEqual(socket.DataType, split.DataType);
            Assert.AreEqual(socket.Name, split.Name);
            Assert.AreEqual(2, split.Children.Count);
            Assert.IsNull(split.Parent);
        }

        [Test]
        public void SplitSocket_HasChildren()
        {
            Node node = new MethodNode();
            DataSocket socket = new DataSocket(node, typeof(StructWithTwoFields));
            SplitSocket split = new SplitSocket(socket);

            Assert.AreEqual(2, split.Children.Count);
            Assert.AreEqual(split, split.Children[0].Parent);
            Assert.AreEqual(nameof(StructWithTwoFields.SomeString), split.Children[0].Name);
            Assert.AreEqual(split.Node, split.Children[0].Node);
            Assert.AreEqual(typeof(string), split.Children[0].DataType);
        }

        #region Generic

        [Test]
        public void GenericSocket_ParameterObserversCount_IsTwo()
        {
            Node node = new MethodNode();

            Parameter param = new GenericArgument(TypeConstraints.None);
            GenericSocket socket = new GenericSocket(node, param);
            GenericSocket socket2 = new GenericSocket(node, param);

            Assert.AreEqual(2, param.Observers.Count);
        }

        [Test]
        public void GenericSocket_TestParameterEquality_ReturnsTrue()
        {
            Node node = new MethodNode();

            Parameter param = new GenericArgument(TypeConstraints.None);
            GenericSocket socket = new GenericSocket(node, param);
            GenericSocket socket2 = new GenericSocket(node, param);

            Assert.AreSame(param, socket.Parameter);
            Assert.AreSame(param, socket2.Parameter);
        }

        [Test]
        public void GenericSocket_UseArgumentWithoutRestrictionsToSetValueTypeInSocket_SocketTypeChanges()
        {
            Node node = new MethodNode();

            Parameter param = new GenericArgument(TypeConstraints.None);
            GenericSocket socket = new GenericSocket(node, param);

            Assert.IsNull(socket.DataType);

            bool result = socket.TrySetDataType(typeof(int));

            Assert.IsTrue(result);
            Assert.AreEqual(typeof(int), socket.DataType);
        }

        [Test]
        public void GenericSocket_UseGenericParameter_SocketTypeChanges()
        {
            Node node = new MethodNode();

            Parameter param = new GenericArgument(TypeConstraints.None);
            GenericSocket socket = new GenericSocket(node, param);

            Assert.IsNull(socket.DataType);

            bool result = socket.TrySetDataType(typeof(int));

            Assert.IsTrue(result);
            Assert.AreEqual(typeof(int), socket.DataType);
        }

        #endregion
    }
}
