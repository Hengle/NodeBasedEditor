using System;

namespace VEdit.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsStruct(this Type type)
        {
            return type.IsValueType && !type.IsPrimitive && !type.IsEnum;
        }

        public static bool CanBeDerived(this Type type)
        {
            return type.IsClass && !type.IsSealed;
        }

        public static bool HasDefaultConstructor(this Type type)
        {
            return type.GetConstructor(new Type[0]) != null;
        }
    }
}
