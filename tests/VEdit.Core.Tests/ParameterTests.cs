using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

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
        public void GenericParameter_MissingGenericArgument_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new GenericParameter(typeof(IEnumerable<>)));
        }

        [Test]
        public void GenericParameter_UseTwoArguments()
        {
            GenericArgument arg1 = new GenericArgument(TypeConstraints.None);
            GenericArgument arg2 = new GenericArgument(TypeConstraints.Class);

            GenericParameter param = new GenericParameter(typeof(Dictionary<,>), arg1, arg2);

            Assert.AreEqual(2, param.Arguments.Count);
        }

        [Test]
        public void GenericParameter_ChangeArgumentType()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.None);
            GenericParameter param = new GenericParameter(typeof(List<>), arg);

            arg.TryChangeType(typeof(int));
            Assert.AreEqual(typeof(List<int>), param.Type);

            arg.TryChangeType(typeof(bool));
            Assert.AreEqual(typeof(List<bool>), param.Type);
        }

        [Test]
        public void GenericParameter_MultipleGenericParametersUsingOneArgument_ChangeArgumentType()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.None);
            GenericParameter param1 = new GenericParameter(typeof(List<>), arg);
            GenericParameter param2 = new GenericParameter(typeof(HashSet<>), arg);

            arg.TryChangeType(typeof(int));
            Assert.AreEqual(typeof(List<int>), param1.Type);
            Assert.AreEqual(typeof(HashSet<int>), param2.Type);
        }

        [Test]
        public void GenericParameter_SetType_ThrowsInvalidOperationException()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.None);
            GenericParameter param = new GenericParameter(typeof(List<>), arg);

            Assert.Throws<InvalidOperationException>(() => param.Type = typeof(int));
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
        public void GenericArgument_SetBaseClass_GenericTypeDefinition()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.None);

            Assert.Throws<InvalidOperationException>(() => arg.SetBaseClass(typeof(List<>)));
        }

        [Test]
        public void GenericArgument_ValidClass()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.None);

            arg.SetBaseClass(typeof(List<int>));
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

        [Test]
        public void GenericArgument_TryChangeType_TypeIsGenericTypeDefinition_ReturnsFalse()
        {
            GenericArgument arg = new GenericArgument(TypeConstraints.None);

            bool result = arg.TryChangeType(typeof(List<>));

            Assert.IsFalse(result);
        }
    }
}
