using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Swagger.Net.Extensions;
using Swagger.Net.Models;

namespace Swagger.Net
{
    public class SwaggerGen
    {

        private readonly XmlCommentDocumentationProvider _docProvider;
        public SwaggerGen()
        {
            _docProvider =
                (XmlCommentDocumentationProvider)GlobalConfiguration.Configuration.Services.GetDocumentationProvider();
        }

        /// <summary>
        /// Create a resource listing
        /// </summary>
        /// <param name="controllerContext">Current controller context</param>
        /// <returns>A resrouce listing</returns>
        public static ApiDeclaration CreateApiDeclaration(HttpControllerContext controllerContext)
        {
            var uri = controllerContext.Request.RequestUri;

            return new ApiDeclaration()
            {
                BasePath = uri.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath.TrimEnd('/'),
                ResourcePath = controllerContext.ControllerDescriptor.ControllerName
            };
        }

        /// <summary>
        /// Create an api element 
        /// </summary>
        /// <param name="api">Description of the api via the ApiExplorer</param>
        /// <returns>A resource api</returns>
        public Api CreateApi(ApiDescription api)
        {
            return new Api()
            {
                Path = "/" + api.GetCleanRelativePath(),
                Description = api.Documentation,
                Operations = new List<Operation>()
                {
                    new Operation()
                    {
                        HttpMethod = api.HttpMethod.ToString(),
                        Nickname = api.GetNickname(),
                        Type = api.ActionDescriptor.ReturnType.GetSwaggerType(),
                        Summary = api.Documentation,
                        Notes = _docProvider.GetOperationNotes(api.ActionDescriptor),
                        Parameters = api.ParameterDescriptions.Select(CreateParameter)
                    }
                }
            };
        }

        /// <summary>
        /// Creates an operation parameter
        /// </summary>
        /// <param name="param">Description of a parameter on an operation via the ApiExplorer</param>
        /// <returns>An operation parameter</returns>
        public Parameter CreateParameter(ApiParameterDescription param)
        {
            return new Parameter()
            {
                ParamTypeEnum = param.GetParamType(),
                Name = param.Name,
                Description = param.Documentation,
                Type = param.ParameterDescriptor.ParameterType.GetSwaggerType(),
                Required = !param.ParameterDescriptor.IsOptional,
                Items = param.ParameterDescriptor.ParameterType.IsIEnumerable() ? new Items(){Type = "string"} : null 
            };
        }
    }


}