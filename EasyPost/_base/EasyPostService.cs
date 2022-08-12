using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost._base
{
    public abstract class EasyPostService : WithClient, IEasyPostService
    {
        internal EasyPostService(EasyPostClient client)
        {
            Client = client;
        }

        protected async Task<T> Create<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? apiVersion = null) where T : class, new() => await Request<T>(Method.Post, url, parameters, rootElement, apiVersion);

        protected async Task CreateNoResponse(string url, Dictionary<string, object>? parameters = null, ApiVersion? apiVersion = null) => await Request(Method.Post, url, parameters, apiVersion);

        protected async Task<T> Delete<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? apiVersion = null) where T : class, new() => await Request<T>(Method.Delete, url, parameters, rootElement, apiVersion);

        protected async Task DeleteNoResponse(string url, Dictionary<string, object>? parameters = null, ApiVersion? apiVersion = null) => await Request(Method.Delete, url, parameters, apiVersion);

        protected async Task<T> Get<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? apiVersion = null) where T : class, new() => await Request<T>(Method.Get, url, parameters, rootElement, apiVersion);

        protected async Task<T> List<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? apiVersion = null) where T : class, new() => await Request<T>(Method.Get, url, parameters, rootElement, apiVersion);

        protected async Task<T> Request<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? apiVersion = null) where T : class, new() => await Client!.Request<T>(method, url, parameters, rootElement, apiVersion);

        protected async Task Request(Method method, string url, Dictionary<string, object>? parameters = null, ApiVersion? apiVersion = null) => await Client!.Request(method, url, parameters, apiVersion);

        protected async Task<T> Update<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null, ApiVersion? apiVersion = null) where T : class, new() => await Request<T>(Method.Patch, url, parameters, rootElement, apiVersion);

        protected async Task UpdateNoResponse(string url, Dictionary<string, object>? parameters = null, ApiVersion? apiVersion = null) => await Request(Method.Patch, url, parameters, apiVersion);
    }

    public interface IEasyPostService
    {
    }
}
