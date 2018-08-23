using System;
using System.Collections.Generic;
using System.Linq;

namespace VEdit.Core
{
    // TODO: Make sure a socket cannot appear in multiple socket collections or multiple nodes (e.g set Node property when it is added to a collection and check if it already has a node before adding it)
    [Serializable]
    public abstract class Node
    {
        public Graph Graph { get; set; }

        private readonly HashSet<Parameter> _parameters = new HashSet<Parameter>();
        public IReadOnlyList<Parameter> Parameters => _parameters.ToList();

        private readonly HashSet<Socket> _input = new HashSet<Socket>();
        public IReadOnlyList<Socket> Input => _input.ToList();

        private readonly HashSet<Socket> _output = new HashSet<Socket>();
        public IReadOnlyList<Socket> Output => _output.ToList();

        public virtual bool AddInput(Socket socket)
        {
            if (socket is null)
                throw new ArgumentNullException(nameof(socket));

            return _input.Add(socket);
        }

        public virtual bool AddOutput(Socket socket)
        {
            if (socket is null)
                throw new ArgumentNullException(nameof(socket));

            return _output.Add(socket);
        }

        public virtual bool RemoveInput(Socket socket)
        {
            if (socket is null)
                throw new ArgumentNullException(nameof(socket));

            return _input.Remove(socket);
        }

        public virtual bool RemoveOutput(Socket socket)
        {
            if (socket is null)
                throw new ArgumentNullException(nameof(socket));

            return _output.Remove(socket);
        }

        internal void AddParameter(Parameter parameter)
        {
            _parameters.Add(parameter);
        }
    }
}
