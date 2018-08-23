namespace VEdit.Core.Extensions
{
    public static class NodeExtensions
    {
        public static bool IsGeneric(this Node node) => node.Parameters.Count > 0;
    }
}
