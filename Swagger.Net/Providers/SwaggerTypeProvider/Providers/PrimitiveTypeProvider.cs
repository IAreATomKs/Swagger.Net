using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swagger.Net.Extensions;
using Swagger.Net.Models;

namespace Swagger.Net.Providers.SwaggerTypeProvider.Providers
{
    public class PrimitiveTypeProvider : ISwaggerTypeProvider
    {
        public Dictionary<Type, SwaggerType> SwaggerTypeMapping { get; set; }

        public PrimitiveTypeProvider()
        {
            SwaggerTypeMapping = new Dictionary<Type, SwaggerType>()
            {
                {typeof(string), new SwaggerType{ Type = "string"}},
                {typeof(Guid), new SwaggerType{ Type = "string"}},
                {typeof(int), new SwaggerType{ Type = "integer", Format = "Int32"}},
                {typeof(long), new SwaggerType{ Type = "integer", Format = "Int64"}},
                {typeof(float), new SwaggerType{ Type = "number", Format = "float"}},
                {typeof(double), new SwaggerType{ Type = "number", Format = "decimal"}},
                {typeof(decimal), new SwaggerType{ Type = "number", Format = "decimal"}},
                {typeof(byte), new SwaggerType{ Type = "string", Format = "byte"}},
                {typeof(bool), new SwaggerType{ Type = "boolean"}},
                {typeof(DateTime), new SwaggerType(){ Type = "string,date-time"}}
            };
        }

        public SwaggerType GetSwaggerType(Type type)
        {
            return SwaggerTypeMapping.GetValueOrDefault(type);
        }
    }
}
