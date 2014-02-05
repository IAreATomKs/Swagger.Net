using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Swagger.Net.Models;
using Swagger.Net.Extensions;

namespace Swagger.Net
{
    public class ModelGen
    {
        public static Dictionary<string,Model> CreateModels(IEnumerable<ApiDescription> apiDescriptions, XmlCommentDocumentationProvider docProvider)
        {
            var parameters = apiDescriptions.SelectMany(a => a.ParameterDescriptions)
                                .Where(p => !p.ParameterDescriptor.ParameterType.IsPrimitive());

            var models = new Dictionary<string,Model>();
            foreach (var param in parameters)
            {
                if (!models.ContainsKey(param.ParameterDescriptor.ParameterType.Name))
                {
                    var model = new Model()
                        {
                            Id = param.ParameterDescriptor.ParameterType.Name,
                            Properties = CreateProperties(param.ParameterDescriptor.ParameterType)
                        };
                    models.Add(param.ParameterDescriptor.ParameterType.Name, model);
                }
            }
            return models;
        }

        private static string GetPropertyName(PropertyInfo info)
        {
            var attributes = info.GetCustomAttributes(typeof (JsonPropertyAttribute), true);
            if (attributes.Length > 0)
            {
                var attribute = (JsonPropertyAttribute)attributes.First();
                return attribute.PropertyName;
            }
            return info.Name;
        }

        private static Dictionary<string,SwaggerType> CreateProperties(Type parameterType)
        {
            return parameterType.GetProperties()
                                .ToDictionary(GetPropertyName, p => p.PropertyType.GetSwaggerType());
        }
    }
}
