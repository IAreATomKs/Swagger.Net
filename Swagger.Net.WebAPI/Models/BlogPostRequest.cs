using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Swagger.Net.WebApi.Models
{
    public class BlogPostRequest
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsPublished { get; set; }
    }

    public enum IsPublished
    {
        Yes,
        No
    }
}