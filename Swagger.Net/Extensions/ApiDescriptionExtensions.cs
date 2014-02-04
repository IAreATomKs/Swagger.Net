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
            //return apiDescription.ParameterDescriptions
            //    .Where(p => p.Source == ApiParameterSource.FromUri)
            //    .Aggregate(relativePath, (c, p) => c.Replace(string.Format("{0}={{{0}}}", p.Name.ToLower()), ""))
            //    .TrimEnd('&').RegexReplace(@"\+&","&");
        }
    }
}
