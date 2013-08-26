using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Swagger.Net.Models
{
    public class Items
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
