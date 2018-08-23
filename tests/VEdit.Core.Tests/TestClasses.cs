﻿using System;

namespace VEdit.Core.Tests
{
    // *** DO NOT MODIFY THESE CLASSES, CREATE OTHER CLASSES IF THESE DO NOT FIT YOUR NEEDS *** //

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

    [Serializable]
    public class TestNode : Node
    {

    }

    public class TestNodeBuilder : NodeBuilder<TestNode, TestNodeBuilder>
    {

    }

    [Serializable]
    public class TestGraph : Graph
    {

    }
}
