using System;
using System.Linq;

namespace StatBot.Helpers
{
    public static class TypeExtensions
    {
        public static Type[] WithInterfaces(this Type type)
        {
            return new[] {type}.Concat(type.GetInterfaces()).ToArray();
        }
    }
}