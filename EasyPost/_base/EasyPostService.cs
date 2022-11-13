using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost._base;

public abstract class EasyPostService : IEasyPostService
{
    protected readonly EasyPostClient Client;

    internal EasyPostService(EasyPostClient client) => Client = client;

    /// <summary>
    ///     Make an HTTP request to the EasyPost API and deserialize the response JSON into an object.
    /// </summary>
    /// <param name="method">HTTP method</param>
    /// <param name="url">EasyPost API endpoint (no base url or API version)</param>
    /// <param name="parameters">Optional parameters to include in the request.</param>
    /// <param name="rootElement">Key to narrow to when deserializing the resultant JSON into an object.</param>
    /// <param name="overrideApiVersion">Override API version hit for HTTP request. Defaults to general availability.</param>
    /// <typeparam name="T">Type of object to return from request.</typeparam>
    /// <returns>A T-type object.</returns>
    protected async Task<T> Request<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? overrideApiVersion = null) where T : class => await Client.Request<T>(method, url, overrideApiVersion ?? ApiVersion.Current, parameters, rootElement);

    /// <summary>
    ///     Make an HTTP request to the EasyPost API.
    /// </summary>
    /// <param name="method">HTTP method</param>
    /// <param name="url">EasyPost API endpoint (no base url or API version)</param>
    /// <param name="parameters">Optional parameters to include in the request.</param>
    /// <param name="overrideApiVersion">Override API version hit for HTTP request. Defaults to general availability.</param>
    /// <returns>None</returns>
    protected async Task Request(Method method, string url, Dictionary<string, object>? parameters = null, ApiVersion? overrideApiVersion = null) => await Client.Request(method, url, overrideApiVersion ?? ApiVersion.Current, parameters);
}

public interface IEasyPostService
{
}
