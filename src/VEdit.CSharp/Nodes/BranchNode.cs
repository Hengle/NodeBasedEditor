using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using VEdit.Core;

namespace VEdit.CSharp.Nodes
{
    [Serializable]
    public sealed class BranchNode : CSharpNode
    {
        public DataSocket<bool> Condition { get; }
        public ExecSocket True { get; }
        public ExecSocket False { get; }
        public ExecSocket In { get; }

        public BranchNode()
        {
            Condition = new DataSocket<bool>()
            {
                Name = "Condition"
            };

            True = new ExecSocket()
            {
                Name = "True"
            };

            False = new ExecSocket()
            {
                Name = "False"
            };

            In = new ExecSocket();

            AddInput(Condition);
            AddInput(In);

            AddOutput(True);
            AddOutput(False);
        }
    }
}
