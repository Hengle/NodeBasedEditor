using System;

namespace VEdit.Editor
{
    public class ExitNode : EditableNode
    {
        public static Guid InstanceId = Guid.Parse("995537fa-1da2-42ea-bcfb-2d98a2c1ef99");

        public ExitNode(Graph root) : base(root)
        {
            CanCopy = false;
            Id = InstanceId;
        }

        public override Pin AddDataPin(Type dataType, string name = null)
        {
            return AddDataPin(Direction.Input, dataType, name, IsEditable, IsEditable);
        }

        public override Pin AddExecPin(string name = null)
        {
            return AddExecPin(Direction.Input, name, IsEditable, IsEditable);
        }
    }
}
