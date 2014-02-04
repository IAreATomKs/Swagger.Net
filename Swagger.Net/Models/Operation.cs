using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Swagger.Net.Models
{
    public class Operation
    {
        [JsonProperty(PropertyName = "httpMethod")]
        public string HttpMethod { get; set; }

        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "parameters")]
        public IEnumerable<Parameter> Parameters { get; set; }

        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        //TODO: Add Response messages

    }
}
