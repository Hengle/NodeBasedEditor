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
        public DataSocket Parent { get; private set; }
        public Type DataType { get; }

        public DataSocket(Node node, SocketType type, Type dataType) : base(node, type)
        {
            DataType = dataType;
        }

        private List<DataSocket> _children;
        public IEnumerable<DataSocket> Children
        {
            get
            {
                if (_children == null)
                {
                    SocketExtractor extractor = new SocketExtractor(this, socket => Parent = socket);
                    _children = extractor.Sockets.ToList();
                }
                return _children;
            }
        }
    }

    [Serializable]
    public class DataSocket<T> : DataSocket
    {
        public DataSocket(Node node, SocketType type) : base(node, type, typeof(T))
        {
        }
    }
}
