using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Swagger.Net.Models
{
    public class ResourceListing
    {
        public ResourceListing()
        {
            Apis = new List<Api>();
        }

        [JsonProperty(PropertyName = "apiVersion")]
        public string ApiVersion { get { return Assembly.GetCallingAssembly().GetType().Assembly.GetName().Version.ToString(); } }

        [JsonProperty(PropertyName = "swaggerVersion")]
        public string SwaggerVersion { get { return Constants.SwaggerVersion; } }

        [JsonProperty(PropertyName = "apis")]
        public IEnumerable<Api> Apis { get; set; }
    }
}
