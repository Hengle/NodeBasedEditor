using System;
using VEdit.Common;

namespace VEdit.Editor
{
    public class GetNode : Node
    {
        public static Guid Template = Guid.Parse("df1cc3af-604b-46d4-a13c-d90bd888f8f2");

        private Pin _output;

        public VariableNode Variable { get; }

        // Don't new it, use Variable.CreateGetNode
        public GetNode(VariableNode variable) : base(variable.Graph, Guid.Empty)
        {
            CanCopy = false;

            Variable = variable;
            IsCompact = true;

            X = variable.X - 100;
            Y = variable.Y - 100;

            Name = variable.Name;
            _output = AddDataPin(Direction.Output, variable.Type);

            variable.Renamed += OnRenamed;

            var cmdProvider = ServiceProvider.Get<ICommandProvider>();
            Actions.Add("Go to variable", cmdProvider.Create(GoToVariable));
        }

        private void GoToVariable()
        {
            Variable.Graph.Focus(Variable);
        }

        private void OnRenamed()
        {
            Name = Variable.Name;
        }

        public Pin GetOutput() => Output[0];
    }
}
