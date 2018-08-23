using System;
using System.Collections.Generic;
using System.Linq;

namespace VEdit.Core
{
    [Serializable]
    public abstract class Socket
    {
        public Node Node { get; }

        public virtual string Name { get; set; }

        public Socket(Node node)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
        }
    }

    [Serializable]
    public class ExecSocket : Socket
    {
        public ExecSocket(Node node) : base(node)
        {
        }
    }

    [Serializable]
    public class DataSocket : Socket
    {
        public DataSocket Parent { get; }
        public virtual Type DataType { get; }

        public DataSocket(Node node, Type dataType, DataSocket parent = null) : base(node)
        {
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Parent = parent;
        }
    }

    [Serializable]
    public class DataSocket<T> : DataSocket
    {
        public DataSocket(Node node) : base(node, typeof(T))
        {
        }
    }

    // DataSocket decorator
    [Serializable]
    public class SplitSocket : DataSocket
    {
        private DataSocket _socket;

        public SplitSocket(DataSocket socket) : base(socket.Node, socket.DataType, socket.Parent)
        {
            _socket = socket;
        }

        public override string Name
        {
            get => _socket.Name;
            set => _socket.Name = value;
        }

        private List<DataSocket> _children;
        public IReadOnlyList<DataSocket> Children
        {
            get
            {
                if (_children == null)
                {
                    SocketExtractor extractor = new SocketExtractor(this);
                    _children = extractor.Sockets.ToList();
                }
                return _children;
            }
        }
    }

    [Serializable]
    public class GenericSocket : DataSocket, IParameterObserver
    {
        public Parameter Parameter { get; }
        private Type _dataType;

        public GenericSocket(Node node, Parameter parameter) : base(node, typeof(void), null)
        {
            Parameter = parameter;
            parameter.Observers.Add(this);
            node.AddParameter(parameter);
        }

        public bool TrySetDataType(Type dataType)
        {
            return Parameter is GenericArgument arg && arg.TryChangeType(dataType);
        }

        public void Update(Parameter param)
        {
            _dataType = param.Type;
        }

        public override Type DataType => _dataType;
    }
}
