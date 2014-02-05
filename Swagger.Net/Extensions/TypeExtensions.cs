using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swagger.Net.Models;
using Swagger.Net.Providers.SwaggerTypeProvider;

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

        public static SwaggerType GetSwaggerType(this Type type)
        {
            var provider = new SwaggerProviderService();
            return provider.GetSwaggerType(type);
        }
    }
}
