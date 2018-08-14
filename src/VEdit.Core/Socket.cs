using System;
using System.Collections.Generic;
using System.Linq;

namespace VEdit.Core
{
    public enum SocketType
    {
        Input,
        Output
    }

    [Serializable]
    public abstract class Socket
    {
        public Node Node { get; }

        public string Name { get; set; }
        public SocketType Type { get; }

        public Socket(Node node, SocketType type)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            Type = type;
        }
    }

    [Serializable]
    public class ExecSocket : Socket
    {
        public ExecSocket(Node node, SocketType type) : base(node, type)
        {
        }
    }

    [Serializable]
    public class DataSocket : Socket
    {
        public DataSocket Parent { get; }
        public Type DataType { get; }

        public DataSocket(Node node, SocketType type, Type dataType, DataSocket parent = null) : base(node, type)
        {
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Parent = parent;
        }
    }

    [Serializable]
    public class DataSocket<T> : DataSocket
    {
        public DataSocket(Node node, SocketType type) : base(node, type, typeof(T))
        {
        }
    }

    // DataSocket Decorator
    [Serializable]
    public class SplittableSocket : DataSocket
    {
        private readonly DataSocket _socket;

        public SplittableSocket(DataSocket socket) : base(socket.Node, socket.Type, socket.DataType, socket.Parent)
        {
            _socket = socket;
            Name = socket.Name;
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
}
