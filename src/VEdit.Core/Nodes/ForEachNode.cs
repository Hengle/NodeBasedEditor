using System;
using System.Collections.Generic;

namespace VEdit.Core.Nodes
{
    [Serializable]
    public sealed class ForEachNode : Node
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

            Collection = new GenericSocket(this, enumerable);
            Item = new GenericSocket(this, argument);

            LoopBody = new ExecSocket(this)
            {
                Name = "Loop"
            };

            Completed = new ExecSocket(this)
            {
                Name = "Completed"
            };

            In = new ExecSocket(this);

            AddInput(In);
            AddInput(Collection);

            AddOutput(LoopBody);
            AddOutput(Completed);
            AddOutput(Item);
        }
    }
}
