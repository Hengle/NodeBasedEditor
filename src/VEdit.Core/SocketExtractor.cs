using System;
using System.Collections.Generic;
using System.Reflection;
using VEdit.Core.Extensions;

namespace VEdit.Core
{
    // Splits a struct into multiple sockets based on public fields
    internal class SocketExtractor
    {
        private readonly FieldExtractor _propertyExtractor;
        private readonly Node _node;
        private readonly SocketType _type;
        private readonly DataSocket _socket;

        public SocketExtractor(DataSocket socket)
        {
            _socket = socket ?? throw new ArgumentNullException(nameof(socket));

            Type dataType = socket.DataType.IsStruct() ? socket.DataType : throw new ArgumentException("type is not a struct");

            _propertyExtractor = new FieldExtractor(dataType);
            _node = socket.Node;
            _type = socket.Type;
        }

        public IEnumerable<DataSocket> Sockets
        {
            get
            {
                foreach (var property in _propertyExtractor.Fields)
                {
                    yield return ExtractField(property);
                }
            }
        }

        private DataSocket ExtractField(FieldInfo info)
        {
            return new DataSocket(_node, _type, info.FieldType, _socket)
            {
                Name = info.Name
            };
        }
    }

    internal class FieldExtractor
    {
        private readonly Type _type;
        private readonly BindingFlags _flags = BindingFlags.Public | BindingFlags.Instance;

        public FieldExtractor(Type type)
        {
            _type = type;
        }

        public IEnumerable<FieldInfo> Fields => _type.GetFields(_flags);
    }
}
