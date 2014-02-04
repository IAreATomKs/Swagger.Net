using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;
using Swagger.Net.Attributes;

namespace Swagger.Net.Extensions
{
    public static class HttpControllerDescriptorExtensions
    {
        public static bool ShouldSwaggerIgnore(this HttpControllerDescriptor controllerDescriptor)
        {
            return controllerDescriptor.ControllerType.HasAttribute(typeof(SwaggerIgnoreAttribute));
        }
    }
}
