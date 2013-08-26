using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Swagger.Net.Models
{
    public class Parameter
    {
        [JsonProperty(PropertyName = "paramType")]
        public string ParamType { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "required")]
        public bool Required { get; set; }

        [JsonProperty(PropertyName = "allowMultiple")]
        public bool AllowMultiple { get; set; }

        [JsonProperty(PropertyName = "minimum")]
        public int Minimum { get; set; }

        [JsonProperty(PropertyName = "maximum")]
        public int Maximum { get; set; }

        [JsonProperty(PropertyName = "enum")]
        public IEnumerable<object> Enum { get; set; }

        [JsonProperty(PropertyName = "items")]
        public Items Items { get; set; } 

    }
}
