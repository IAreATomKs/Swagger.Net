using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Description;

namespace Swagger.Net.Extensions
{
    public static class ApiDescriptionExtensions
    {
        public static string GetCleanRelativePath(this ApiDescription apiDescription)
        {
            return apiDescription.RelativePath.TruncateFrom("?").ToLower();
        }

        public static string GetNickname(this ApiDescription apiDescription)
        {
            return apiDescription.ActionDescriptor.ActionName + String.Join("-", apiDescription.ParameterDescriptions.Select(pd => pd.Name));
        }
    }
}
