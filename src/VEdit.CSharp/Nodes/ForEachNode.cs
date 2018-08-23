using System;
using System.Collections.Generic;
using VEdit.Core;

namespace VEdit.CSharp.Nodes
{
    [Serializable]
    public sealed class ForEachNode : CSharpNode
    {
        public ExecSocket In { get; }
        public ExecSocket LoopBody { get; }
        public ExecSocket Completed { get; }

        public GenericSocket Collection { get; }
        public GenericSocket Item { get; }

        public ForEachNode()
        {
            GenericArgument argument = new GenericArgument(TypeConstraints.None);
            GenericParameter enumerable = new GenericParameter(typeof(IEnumerable<>), argument);

            Collection = new GenericSocket(enumerable);
            Item = new GenericSocket(argument);

            LoopBody = new ExecSocket()
            {
                Name = "Loop"
            };

            Completed = new ExecSocket()
            {
                Name = "Completed"
            };

            In = new ExecSocket();

            AddInput(In);
            AddInput(Collection);

            AddOutput(LoopBody);
            AddOutput(Completed);
            AddOutput(Item);
        }
    }
}
