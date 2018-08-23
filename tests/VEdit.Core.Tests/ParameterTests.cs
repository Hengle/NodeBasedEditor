using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using VEdit.Core.Nodes;

namespace VEdit.Core.Tests
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void GenericParameter_UseConstructedType_ThrowsArgumentException()
        {
            Parameter arg = new GenericArgument(TypeConstraints.None);

            Assert.Throws<ArgumentException>(() => new GenericParameter(typeof(int), arg));
            Assert.Throws<ArgumentException>(() => new GenericParameter(typeof(IEnumerable<int>), arg));
        }

        [Test]
        public void GenericArgument_AddInterface_DuplicateInterface_DoesNotAdd()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.None);

            arg.AddInterface(typeof(IEnumerable));
            arg.AddInterface(typeof(IEnumerable));

            Assert.AreEqual(1, arg.Interfaces.Count);
        }

        [Test]
        public void GenericArgument_AddInterface_ArgumentIsNotAnInterface_ThrowsInvalidOperationException()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.None);

            Assert.Throws<InvalidOperationException>(() => arg.AddInterface(typeof(int)));
            Assert.Throws<InvalidOperationException>(() => arg.AddInterface(typeof(Node)));
        }

        [Test]
        public void GenericArgument_SetBaseClass_ArgumentIsNotAValidClass_ThrowsInvalidOperationException()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.None);

            Assert.Throws<InvalidOperationException>(() => arg.SetBaseClass(typeof(int)));
            Assert.Throws<InvalidOperationException>(() => arg.SetBaseClass(typeof(StructWithTwoFields)));
        }
        [Test]
        public void GenericParameter_NoGenericArgument_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new GenericParameter(typeof(IEnumerable<>)));
        }

        [Test]
        public void GenericArgument_TryChangeType_WithNewConstraint_TypeDoesNotHaveADefaultConstructor_ReturnsFalse()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.New);

            bool result = arg.TryChangeType(typeof(ClassWithoutDefaultConstructor));

            Assert.IsFalse(result);
        }

        [Test]
        public void GenericArgument_TryChangeType_WithClassConstraint_TypeIsNotClass_ReturnsFalse()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.Class);

            bool result = arg.TryChangeType(typeof(IEnumerable));

            Assert.IsFalse(result);
        }

        [Test]
        public void GenericArgument_TryChangeType_WithNewAndClassConstraints_TypeIsValid_ReturnsTrue()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.Class | TypeConstraints.New);

            bool result = arg.TryChangeType(typeof(ClassWithDefaultConstructor));

            Assert.IsTrue(result);
        }

    }
}
