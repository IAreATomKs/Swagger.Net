using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swagger.Net.Models;

namespace Swagger.Net.Providers.SwaggerTypeProvider.Providers
{
    public class EnumTypeProvider : ISwaggerTypeProvider
    {
        public SwaggerType GetSwaggerType(Type type)
        {
            if (type.IsEnum)
            {
                return new SwaggerType
                {
                    Type = "string",
                    Enum = type.GetEnumNames().AsEnumerable()
                };
            }
            return null;
        }
    }
}
