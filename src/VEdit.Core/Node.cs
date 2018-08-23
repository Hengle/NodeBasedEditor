using System;
using System.Collections.Generic;
using System.Linq;

namespace VEdit.Core
{
    [Serializable]
    public abstract class Node
    {
        public Graph Graph { get; set; }

        private readonly HashSet<Parameter> _parameters = new HashSet<Parameter>();
        public IReadOnlyList<Parameter> Parameters => _parameters.ToList();

        private readonly List<Socket> _input = new List<Socket>();
        public IReadOnlyList<Socket> Input => _input;

        private readonly List<Socket> _output = new List<Socket>();
        public IReadOnlyList<Socket> Output => _output;

        public virtual void AddInput(Socket socket)
        {
            if (socket is null)
                throw new ArgumentNullException(nameof(socket));

            _input.Add(socket);
        }

        public virtual void AddOutput(Socket socket)
        {
            if (socket is null)
                throw new ArgumentNullException(nameof(socket));

            _output.Add(socket);
        }

        public virtual void RemoveInput(Socket socket)
        {
            if (socket is null)
                throw new ArgumentNullException(nameof(socket));

            _input.Remove(socket);
        }

        public virtual void RemoveOutput(Socket socket)
        {
            if (socket is null)
                throw new ArgumentNullException(nameof(socket));

            _output.Remove(socket);
        }

        internal void AddParameter(Parameter parameter)
        {
            _parameters.Add(parameter);
        }
    }
}
