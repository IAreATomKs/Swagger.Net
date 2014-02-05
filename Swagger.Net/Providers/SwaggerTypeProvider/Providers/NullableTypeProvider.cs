using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swagger.Net.Models;

namespace Swagger.Net.Providers.SwaggerTypeProvider.Providers
{
    public class NullableTypeProvider : ISwaggerTypeProvider
    {
        public SwaggerType GetSwaggerType(Type type)
        {
            var primitiveType = Nullable.GetUnderlyingType(type);
            if (primitiveType != null)
            {
                var primitiveProvider = new PrimitiveTypeProvider();
                return primitiveProvider.GetSwaggerType(primitiveType);
            }
            return null;
        }
    }
}
