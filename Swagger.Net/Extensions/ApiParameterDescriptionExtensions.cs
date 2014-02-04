using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Description;

namespace Swagger.Net.Extensions
{
    public static class ApiParameterDescriptionExtensions
    {
        public static ParamType GetParamType(this ApiParameterDescription parameterDescription)
        {
            switch (parameterDescription.Source)
            {
                case ApiParameterSource.FromBody:
                    return ParamType.Body;

                default:
                case ApiParameterSource.FromUri:
                case ApiParameterSource.Unknown:
                    return parameterDescription.ParameterDescriptor.IsOptional ? ParamType.Query : ParamType.Path;
            }
        }
    }
}
