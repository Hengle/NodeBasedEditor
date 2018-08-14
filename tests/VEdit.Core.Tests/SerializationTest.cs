using NUnit.Framework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class SerializationTest
    {
        private struct Test
        {
            public string Text;
            public int Value;
        }

        [Test]
        public void Serialize_GenericNode_DoesNotThrow()
        {
            Test test;
            test.Text = "asd";

            NodeBuilder<GenericNode, GenericNodeBuilder> _builder = new GenericNodeBuilder();
            var node = _builder
                .AddInput<Test>("Parent")
                .AddInput<bool>()
                .Build();

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, node);

                stream.Seek(0, SeekOrigin.Begin);

                Node result = (GenericNode)formatter.Deserialize(stream);
            }
        }
    }
}
