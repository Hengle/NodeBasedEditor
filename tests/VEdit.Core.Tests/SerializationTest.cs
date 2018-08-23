using NUnit.Framework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using VEdit.Core.Nodes;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class SerializationTest
    {
        [Test]
        public void Serialize_GenericNode_DoesNotThrow()
        {
            StructWithTwoFields test;
            test.SomeString = "asd";
            test.SomeInt = 5;

            var builder = new MethodNodeBuilder();
            var node = builder
                .AddInput<StructWithTwoFields>("Parent")
                .AddInput<bool>()
                .Build();

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, node);

                stream.Seek(0, SeekOrigin.Begin);

                Node result = (MethodNode)formatter.Deserialize(stream);
            }
        }
    }
}
