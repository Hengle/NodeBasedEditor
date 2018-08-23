using System;
using VEdit.Core;

namespace VEdit.CSharp.Tests
{
    // *** DO NOT MODIFY THESE CLASSES, CREATE OTHER CLASSES IF THESE DO NOT FIT YOUR NEEDS *** //

    [Serializable]
    public class TestNode : Node
    {

    }

    public struct StructWithTwoFields
    {
        public string SomeString;
        public int SomeInt;
    }

    public class ClassWithoutDefaultConstructor
    {
        public ClassWithoutDefaultConstructor(string str)
        {

        }
    }

    public class ClassWithDefaultConstructor
    {

    }
}
