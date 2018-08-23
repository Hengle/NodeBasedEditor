using VEdit.Core.Extensions;
using VEdit.Core.Nodes;

namespace VEdit.Core
{
    public abstract class Builder<T, TBuilder>
        where T: class, new()
        where TBuilder: Builder<T, TBuilder>
    {
        protected T Object;
        protected readonly TBuilder _this;

        public Builder()
        {
            Object = new T();
            _this = (TBuilder)this;
        }

        public T Build()
        {
            T result = Object;
            Object = null;
            return result;
        }
    }

    public abstract class NodeBuilder<TNode, TBuilder> : Builder<TNode, TBuilder>
        where TNode: Node, new()
        where TBuilder: NodeBuilder<TNode, TBuilder>
    {
        public TBuilder AddInput<T>(string name = null)
        {
            var socket = new DataSocket<T>(Object)
            {
                Name = name
            };

            if (typeof(T).IsStruct())
            {
                Object.AddInput(new SplitSocket(socket));
            }
            else
            {
                Object.AddInput(socket);
            }

            return _this;
        }

        public TBuilder AddOutput<T>(string name = null)
        {
            var socket = new DataSocket<T>(Object)
            {
                Name = name
            };

            if (typeof(T).IsStruct())
            {
                Object.AddOutput(new SplitSocket(socket));
            }
            else
            {
                Object.AddOutput(socket);
            }

            return _this;
        }
    }

    public class MethodNodeBuilder : NodeBuilder<MethodNode, MethodNodeBuilder>
    {

    }
}
