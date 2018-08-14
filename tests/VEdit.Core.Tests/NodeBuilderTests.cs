using System;
using NUnit.Framework;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class NodeBuilderTests
    {
        private struct SomeStruct
        {
            public string SomeString;
        }

        [Test]
        public void Build_ReturnsNode()
        {
            var builder = new GenericNodeBuilder();
            var result = builder.Build();

            Assert.IsInstanceOf<Node>(result);
        }

        [Test]
        public void AddInput_ChainTwoTimes_CreatesTwoSockets()
        {
            var builder = new GenericNodeBuilder();

            var result = builder
                .AddInput<int>()
                .AddInput<bool>()
                .Build();

            Assert.AreEqual(2, result.InputData.Count);
        }

        [Test]
        public void Build_Reuse_ReturnsNull()
        {
            var builder = new GenericNodeBuilder();

            var result = builder
                .AddInput<int>()
                .Build();

            result = builder.Build();

            Assert.IsNull(result);
        }

        [Test]
        public void AddInput_StructType_CreatesSplittableSocket()
        {
            var builder = new GenericNodeBuilder();

            var result = builder
                .AddInput<SomeStruct>()
                .Build();

            Assert.IsInstanceOf(typeof(SplittableSocket), result.InputData[0]);
        }
    }
}
