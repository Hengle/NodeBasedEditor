using NUnit.Framework;
using VEdit.Core;

namespace VEdit.CSharp.Tests
{
    [TestFixture]
    public class GenericSocketTests
    {
        [Test]
        public void GenericSocket_ParameterObserversCount_IsTwo()
        {
            Node node = new TestNode();

            Parameter param = new GenericArgument(TypeConstraints.None);
            GenericSocket socket = new GenericSocket(param);
            GenericSocket socket2 = new GenericSocket(param);

            Assert.AreEqual(2, param.Observers.Count);
        }

        [Test]
        public void GenericSocket_TestParameterEquality_ReturnsTrue()
        {
            Node node = new TestNode();

            Parameter param = new GenericArgument(TypeConstraints.None);
            GenericSocket socket = new GenericSocket(param);
            GenericSocket socket2 = new GenericSocket(param);

            Assert.AreSame(param, socket.Parameter);
            Assert.AreSame(param, socket2.Parameter);
        }

        [Test]
        public void GenericSocket_UseArgumentWithoutRestrictionsToSetValueTypeInSocket_SocketTypeChanges()
        {
            Node node = new TestNode();

            Parameter param = new GenericArgument(TypeConstraints.None);
            GenericSocket socket = new GenericSocket(param);

            Assert.IsNull(socket.DataType);

            bool result = socket.TrySetDataType(typeof(int));

            Assert.IsTrue(result);
            Assert.AreEqual(typeof(int), socket.DataType);
        }

        [Test]
        public void GenericSocket_UseGenericParameter_SocketTypeChanges()
        {
            Node node = new TestNode();

            Parameter param = new GenericArgument(TypeConstraints.None);
            GenericSocket socket = new GenericSocket(param);

            Assert.IsNull(socket.DataType);

            bool result = socket.TrySetDataType(typeof(int));

            Assert.IsTrue(result);
            Assert.AreEqual(typeof(int), socket.DataType);
        }

    }
}
