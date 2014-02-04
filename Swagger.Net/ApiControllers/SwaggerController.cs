using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Swagger.Net.Attributes;
using Swagger.Net.Extensions;
using Swagger.Net.Models;

namespace Swagger.Net.ApiControllers
{
    [SwaggerIgnore]
    public class SwaggerController : ApiController
    {
        private readonly IEnumerable<ApiDescription> _apiDescriptions;
        private readonly XmlCommentDocumentationProvider _docProvider;

        public SwaggerController()
        {
            _apiDescriptions = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions
                .Where(s => !s.ActionDescriptor.ControllerDescriptor.ShouldSwaggerIgnore());
            _docProvider =
                (XmlCommentDocumentationProvider) GlobalConfiguration.Configuration.Services.GetDocumentationProvider();
        }

        /// <summary>
        /// Get the resource description of the api for swagger documentation
        /// </summary>
        /// <remarks>It is very convenient to have this information available for generating clients. This is the entry point for the swagger UI
        /// </remarks>
        /// <returns>JSON document representing structure of API</returns>
        public ResourceListing Get()
        {
            var controllers = _apiDescriptions.Select(a => a.ActionDescriptor.ControllerDescriptor)
                .Distinct();
            return new ResourceListing()
                {
                    Apis = controllers.Select(controller => new Api()
                    {
                        //TODO: Fix path with proper routing
                        Path = "/getapi/" + controller.ControllerName,
                        Description = controller.
                    })
                };
        }

        public ApiDeclaration GetApi(string controllerName)
        {
            var r = SwaggerGen.CreateApiDeclaration(ControllerContext);
            var actions = _apiDescriptions.Where(
                    a => a.ActionDescriptor.ControllerDescriptor.ControllerName
                            .Equals(controllerName,StringComparison.InvariantCultureIgnoreCase)
                );

            r.Apis = actions.Select(api => SwaggerGen.CreateApi(api, _docProvider));
            r.Models = ModelGen.CreateModels(actions, _docProvider);

            return r;
        }
    }
}
