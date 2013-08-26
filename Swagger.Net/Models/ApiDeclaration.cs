using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Swagger.Net.Models
{
    public class ApiDeclaration
    {
        public ApiDeclaration()
        {
            Apis = new List<Api>();
        }

        [JsonProperty(PropertyName = "apiVersion")]
        public string ApiVersion { get { return Assembly.GetCallingAssembly().GetType().Assembly.GetName().Version.ToString(); } }

        [JsonProperty(PropertyName = "swaggerVersion")]
        public string SwaggerVersion { get { return Constants.SwaggerVersion; } }

        [JsonProperty(PropertyName = "basePath")]
        public string BasePath { get; set; }

        [JsonProperty(PropertyName = "resourcePath")]
        public string ResourcePath { get; set; }

        [JsonProperty(PropertyName = "apis")]
        public IEnumerable<Api> Apis { get; set; }

        [JsonProperty(PropertyName = "models")]
        public Dictionary<string, Model> Models { get; set; } 

    }
}
