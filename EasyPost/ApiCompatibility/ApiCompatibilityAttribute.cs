using System;
using System.Collections;
using EasyPost.Clients;
using EasyPost.Utilities;

namespace EasyPost.ApiCompatibility
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    internal class ApiCompatibilityAttribute : BaseCustomAttribute
    {
        /// <summary>
        ///     The API versions that this property is compatible with.
        /// </summary>
        private ApiVersion[] ApiVersions { get; }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="apiVersions">API versions that is property is compatible with.</param>
        public ApiCompatibilityAttribute(params ApiVersion[] apiVersions)
        {
            ApiVersions = apiVersions;
        }

        /// <summary>
        ///     Get whether the property is compatible with the specified API version.
        /// </summary>
        /// <param name="apiVersion">Attempted API version.</param>
        /// <returns>True if the property is compatible with the provided API version.</returns>
        public bool IsCompatible(ApiVersion apiVersion)
        {
            return ((IList)ApiVersions).Contains(apiVersion);
        }
    }
}
