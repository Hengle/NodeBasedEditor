using System;

namespace VEdit.Core.Nodes
{
    [Serializable]
    public sealed class BranchNode : Node
    {
        public DataSocket<bool> Condition { get; }
        public ExecSocket True { get; }
        public ExecSocket False { get; }
        public ExecSocket In { get; }

        public BranchNode()
        {
            Condition = new DataSocket<bool>(this)
            {
                Name = "Condition"
            };

            True = new ExecSocket(this)
            {
                Name = "True"
            };

            False = new ExecSocket(this)
            {
                Name = "False"
            };

            In = new ExecSocket(this);

            AddInput(Condition);
            AddInput(In);

            AddOutput(True);
            AddOutput(False);
        }
    }
}
