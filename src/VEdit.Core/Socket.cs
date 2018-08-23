using System;
using System.Collections.Generic;
using System.Linq;

namespace VEdit.Core
{
    [Serializable]
    public abstract class Socket
    {
        public virtual Node Node { get; internal set; }
        public virtual string Name { get; set; }
    }

    [Serializable]
    public class ExecSocket : Socket
    {

    }

    [Serializable]
    public class DataSocket : Socket
    {
        public DataSocket Parent { get; }
        public virtual Type DataType { get; }

        public DataSocket(Type dataType, DataSocket parent = null)
        {
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Parent = parent;
        }
    }

    [Serializable]
    public class DataSocket<T> : DataSocket
    {
        public DataSocket() : base(typeof(T))
        {
        }
    }

    // DataSocket decorator
    [Serializable]
    public class SplitSocket : DataSocket
    {
        private DataSocket _socket;

        public SplitSocket(DataSocket socket) : base(socket.DataType, socket.Parent)
        {
            _socket = socket;
        }

        public override string Name
        {
            get => _socket.Name;
            set => _socket.Name = value;
        }

        public override Node Node
        {
            get => _socket.Node;
            internal set => _socket.Node = value;
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

        public GenericSocket(Parameter parameter) : base(typeof(void), null)
        {
            Parameter = parameter;
            parameter.AddObserver(this);
        }

        public bool TrySetDataType(Type dataType)
        {
            return Parameter.TryChangeType(dataType);
        }

        public void Update(Parameter param)
        {
            _dataType = param.Type;
        }

        public override Type DataType => _dataType;
    }
}
