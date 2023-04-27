using System.Collections.Generic;
using System.Threading.Tasks;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    public abstract class EasyPostService : IEasyPostService
    {
        protected internal readonly EasyPostClient Client;
        
#pragma warning disable IDE0021
        internal EasyPostService(EasyPostClient client)
        {
            Client = client;
        }
#pragma warning restore IDE0021
        
        /// <summary>
        ///     Make an HTTP request to the EasyPost API and deserialize the response JSON into an object.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <param name="endpoint">EasyPost API endpoint (no base url or API version).</param>
        /// <param name="parameters">Optional parameters to include in the request.</param>
        /// <param name="rootElement">Key to narrow to when deserializing the resultant JSON into an object.</param>
        /// <param name="overrideApiVersion">Override API version hit for HTTP request. Defaults to general availability.</param>
        /// <typeparam name="T">Type of object to return from request.</typeparam>
        /// <returns>A T-type object.</returns>
        protected async Task<T> Request<T>(Http.Method method, string endpoint, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? overrideApiVersion = null)
            where T : class
            => await Client.Request<T>(method, endpoint, overrideApiVersion ?? ApiVersion.Current, parameters, rootElement);

        /// <summary>
        ///     Make an HTTP request to the EasyPost API.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <param name="url">EasyPost API endpoint (no base url or API version).</param>
        /// <param name="parameters">Optional parameters to include in the request.</param>
        /// <param name="overrideApiVersion">Override API version hit for HTTP request. Defaults to general availability.</param>
        /// <returns>None.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        protected async Task Request(Http.Method method, string url, Dictionary<string, object>? parameters = null, ApiVersion? overrideApiVersion = null) => await Client.Request(method, url, overrideApiVersion ?? ApiVersion.Current, parameters);

    }

    public interface IEasyPostService
    {
    }
}
