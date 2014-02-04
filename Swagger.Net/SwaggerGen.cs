using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Swagger.Net.Extensions;
using Swagger.Net.Models;

namespace Swagger.Net
{
    public static class SwaggerGen
    {

        /// <summary>
        /// Create a resource listing
        /// </summary>
        /// <param name="controllerContext">Current controller context</param>
        /// <param name="includeResourcePath">Should the resource path property be included in the response</param>
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
        public static Api CreateApi(ApiDescription api, XmlCommentDocumentationProvider docProvider)
        {
            var rApi = new Api()
            {
                Path = "/" + api.GetCleanRelativePath(),
                Description = api.Documentation,
                Operations = new List<Operation>()
            };


            Operation rApiOperation = CreateOperation(api, docProvider);
            rApi.Operations.Add(rApiOperation);

            foreach (var param in api.ParameterDescriptions)
            {
                Parameter parameter = CreateParameter(api, param, docProvider);
                rApiOperation.Parameters.Add(parameter);
            }

            return rApi;
        }

        /// <summary>
        /// Creates an api operation
        /// </summary>
        /// <param name="api">Description of the api via the ApiExplorer</param>
        /// <param name="docProvider">Access to the XML docs written in code</param>
        /// <returns>An api operation</returns>
        public static Operation CreateOperation(ApiDescription api, XmlCommentDocumentationProvider docProvider)
        {
            var rApiOperation = new Operation()
            {
                HttpMethod = api.HttpMethod.ToString(),
                Nickname = docProvider.GetOperationNickname(api),
                Type = api.ActionDescriptor.ReturnType.GetSwaggerType(),
                Summary = api.Documentation,
                Notes = docProvider.GetOperationNotes(api.ActionDescriptor),
                Parameters = new List<Parameter>()
            };

            return rApiOperation;
        }

        /// <summary>
        /// Creates an operation parameter
        /// </summary>
        /// <param name="api">Description of the api via the ApiExplorer</param>
        /// <param name="param">Description of a parameter on an operation via the ApiExplorer</param>
        /// <param name="docProvider">Access to the XML docs written in code</param>
        /// <returns>An operation parameter</returns>
        public static Parameter CreateParameter(ApiDescription api, ApiParameterDescription param, XmlCommentDocumentationProvider docProvider)
        {
            var parameter = new Parameter()
            {
                ParamTypeEnum = param.GetParamType(),
                Name = param.Name,
                Description = param.Documentation,
                Type = param.ParameterDescriptor.ParameterType.GetSwaggerType(),
                Required = docProvider.GetRequired(param.ParameterDescriptor),
                Items = param.ParameterDescriptor.ParameterType.IsIEnumerable() ? new Items(){Type = "string"} : null 
            };

            return parameter;
        }
    }


}