using System;
using System.Reflection;
using System.Linq;

namespace VEdit.Editor
{
    public class PluginNode : Node
    {
        public MethodInfo Method { get; }

        public PluginNode(Graph root, Guid templatedId, bool isCompact, MethodInfo method) : base(root, templatedId)
        {
            IsCompact = isCompact;
            Method = method;
        }
    }
}
