using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Utilities;
using RestSharp;

namespace EasyPost.Interfaces
{
    public class Service : IService
    {
        internal readonly Client Client;

        internal Service(Client client)
        {
            Client = client;
        }

        protected async Task<T> Create<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Client.Request<T>(Method.Post, url, parameters, rootElement);

        protected async Task<bool> CreateBlind(string url, Dictionary<string, object>? parameters = null) => await Client.Request(Method.Post, url, parameters);

        protected async Task<T> Delete<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Client.Request<T>(Method.Delete, url, parameters, rootElement);

        protected async Task<bool> DeleteBlind(string url, Dictionary<string, object>? parameters = null) => await Client.Request(Method.Delete, url, parameters);

        protected async Task<T> Get<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Client.Request<T>(Method.Get, url, parameters, rootElement);

        protected async Task<T> List<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Client.Request<T>(Method.Get, url, parameters, rootElement);

        protected async Task<T> Update<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Client.Request<T>(Method.Patch, url, parameters, rootElement);

        protected async Task<bool> UpdateBlind(string url, Dictionary<string, object>? parameters = null) => await Client.Request(Method.Patch, url, parameters);

        private void CheckCompatibilityWithApiVersion(string methodName)
        {
            // get property info
            MethodInfo? methodInfo = GetType().GetMethod(methodName) ?? null;
            if (methodInfo == null)
            {
                throw new Exception($"Method {methodName} not found on {GetType().Name}");
            }

            ApiCompatibilityAttribute? apiCompatibilityAttribute = BaseCustomAttribute.GetCustomAttribute<ApiCompatibilityAttribute>(methodInfo);
            if (apiCompatibilityAttribute == null)
            {
                // if property does not have an API compatibility attribute, it is compatible with all API versions
                return;
            }

            // throw exception if property is not compatible with this API version
            if (!apiCompatibilityAttribute.IsCompatible(Client.ApiVersion))
            {
                throw new ApiVersionNotSupported(methodName, Client.ApiVersion);
            }
        }
    }

    public interface IService
    {
    }
}
