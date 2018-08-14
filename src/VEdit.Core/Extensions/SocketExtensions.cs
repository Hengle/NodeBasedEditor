namespace VEdit.Core.Extensions
{
    public static class SocketExtensions
    {
        public static bool HasParent(this DataSocket socket) => socket.Parent != null;
    }
}
