using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swagger.Net.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsPrimitive(this Type type)
        {
            return type.IsPrimitive || type == typeof(Decimal) || type == typeof(String) || type == typeof(DateTime);
        }

        public static bool IsIEnumerable(this Type type)
        {
            return type != typeof(string) && type == typeof(IEnumerable<>);
        }

        public static bool HasAttribute(this Type type, Type attributeType)
        {
            return type.GetCustomAttributes(attributeType, true).Any();
        }

        public static string GetSwaggerType(this Type type)
        {
            if (type == null)
            {
                return "void";
            }
            if (type.IsIEnumerable())
            {
                return "Array";
            }
            //TODO: Add other format for guids
            if (type == typeof(string) || type == typeof(Guid))
            {
                return "string";
            }
            if (type == typeof(int))
            {
                return "integer";
            }
            if (type == typeof(long))
            {
                return "long";
            }
            if (type == typeof(float))
            {
                return "float";
            }
            if (type == typeof(double) || type == typeof(decimal))
            {
                return "double";
            }
            if (type == typeof(byte))
            {
                return "byte";
            }
            if (type == typeof(bool))
            {
                return "boolean";
            }
            if (type == typeof(DateTime))
            {
                return "datetime";
            }
            if (!type.IsPrimitive())
            {
                return type.Name;
            }
            return "string";
        }
    }
}
