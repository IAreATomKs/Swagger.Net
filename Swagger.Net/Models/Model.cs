using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Swagger.Net.Models
{
    public class Model
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "required")]
        public IEnumerable<string> Required { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public Dictionary<string, SwaggerType> Properties { get; set; }
    }
}
