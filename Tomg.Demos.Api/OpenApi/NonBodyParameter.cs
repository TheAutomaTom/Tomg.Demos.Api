using Microsoft.OpenApi.Models;

namespace Tomg.Demos.Api.OpenApi
{
    /// <summary>
    /// Parameter without body.
    /// </summary>
    public class NonBodyParameter : OpenApiParameter
    {
        /// <summary>
        /// Default.
        /// </summary>
        public object Default { get; set; }
    }
}