using NUnit.Framework;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class NodeBuilderTests
    {
        [Test]
        public void Build_ReturnsNode()
        {
            var builder = new MethodNodeBuilder();
            var result = builder.Build();

            Assert.IsInstanceOf<Node>(result);
        }

        [Test]
        public void AddInput_ChainTwoTimes_CreatesTwoSockets()
        {
            var builder = new MethodNodeBuilder();

            var result = builder
                .AddInput<int>()
                .AddInput<bool>()
                .Build();

            Assert.AreEqual(2, result.Input.Count);
        }

        [Test]
        public void Build_Reuse_ReturnsNull()
        {
            var builder = new MethodNodeBuilder();

            var result = builder
                .AddInput<int>()
                .Build();

            result = builder.Build();

            Assert.IsNull(result);
        }

        [Test]
        public void AddInput_StructType_CreatesSplittableSocket()
        {
            var builder = new MethodNodeBuilder();

            var result = builder
                .AddInput<StructWithTwoFields>()
                .Build();

            Assert.IsInstanceOf(typeof(SplitSocket), result.Input[0]);
        }
    }
}
