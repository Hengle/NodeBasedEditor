using NUnit.Framework;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using VEdit.Core.Nodes;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class SerializationTest
    {
        class Test
        {
            public string Text { get; set; }
        }

        [Test]
        public void Serialize_ValidNode_DoesNotThrow()
        {
            NodeBuilder<GenericNode, GenericNodeBuilder> _builder = new GenericNodeBuilder();
            var node = _builder
                .AddInput<Test>()
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
