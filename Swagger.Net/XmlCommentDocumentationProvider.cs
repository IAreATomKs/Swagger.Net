using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Swagger.Net
{
    /// <summary>
    /// Accesses the XML doc blocks written in code to further document the API.
    /// All credit goes to: <see cref="http://blogs.msdn.com/b/yaohuang1/archive/2012/05/21/asp-net-web-api-generating-a-web-api-help-page-using-apiexplorer.aspx"/>
    /// </summary>
    public class XmlCommentDocumentationProvider : IDocumentationProvider
    {
        private static readonly Regex NullableTypeNameRegex = new Regex(@"(.*\.Nullable)" + Regex.Escape("`1[[") + "([^,]*),.*");

        private readonly XElement _xDoc;
        private readonly XElement _xMembers;

        public XmlCommentDocumentationProvider(string documentPath)
        {
            _xDoc = XElement.Load(documentPath);
            _xMembers = _xDoc.Element("members");
        }

        public virtual string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            return GetMember(actionDescriptor).Summary;
        }

        public virtual string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            var reflectedParameterDescriptor = parameterDescriptor as ReflectedHttpParameterDescriptor;

            if (reflectedParameterDescriptor == null) return null;

            var member = GetMember(reflectedParameterDescriptor.ActionDescriptor);
            var parameter =
                member.Parameters.FirstOrDefault(p => p.Name == reflectedParameterDescriptor.ParameterInfo.Name);

            return parameter != null ? parameter.Value : null;
        }

        public virtual string GetOperationNotes(HttpActionDescriptor actionDescriptor)
        {
            return GetMember(actionDescriptor).Remarks;
        }

        private Member GetMember(HttpActionDescriptor actionDescriptor)
        {
            var reflectedActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;
            if (reflectedActionDescriptor == null)
                return new Member();


            var xMember = _xMembers.Elements().FirstOrDefault(x => x.Attribute("name").Value == "M:" + GetMemberName(reflectedActionDescriptor.MethodInfo));
            if (xMember == null)
                return new Member();

            var xSummary = xMember.Element("summary");
            var xRemarks = xMember.Element("remarks");

            return new Member()
            {
                Summary = xSummary != null ? xSummary.Value : null,
                Remarks = xRemarks != null ? xRemarks.Value : null,
                Parameters = xMember.Elements("param").Select(p => new XParameter()
                {
                    Name = p.Attribute("name").Value,
                    Value = p.Value
                })
            };
        }

        private static string GetMemberName(MethodInfo method)
        {
            var name = string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name);
            var parameters = method.GetParameters();
            if (parameters.Length != 0)
            {
                var parameterTypeNames = parameters.Select(param => ProcessTypeName(param.ParameterType.FullName)).ToArray();
                name += string.Format("({0})", string.Join(",", parameterTypeNames));
            }

            return name;
        }

        private static string ProcessTypeName(string typeName)
        {
            //handle nullable
            var result = NullableTypeNameRegex.Match(typeName);
            return result.Success 
                ? string.Format("{0}{{{1}}}", result.Groups[1].Value, result.Groups[2].Value) 
                : typeName;
        }
    }

    public class Member
    {
        public Member()
        {
            Parameters = new List<XParameter>();
        }

        public string Summary { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<XParameter> Parameters { get; set; }
    }

    public class XParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
