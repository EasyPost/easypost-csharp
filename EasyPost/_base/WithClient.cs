using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost._base
{
    public abstract class WithClient
    {
        [JsonIgnore]
        internal EasyPostClient? Client { get; set; }

        protected async Task<T> Create<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Request<T>(Method.Post, url, parameters, rootElement);

        protected async Task CreateNoResponse(string url, Dictionary<string, object>? parameters = null) => await Request(Method.Post, url, parameters);

        protected async Task<T> Delete<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Request<T>(Method.Delete, url, parameters, rootElement);

        protected async Task DeleteNoResponse(string url, Dictionary<string, object>? parameters = null) => await Request(Method.Delete, url, parameters);

        protected async Task<T> Get<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Request<T>(Method.Get, url, parameters, rootElement);

        protected async Task<T> List<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Request<T>(Method.Get, url, parameters, rootElement);

        protected async Task<T> Request<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class => await Client!.Request<T>(method, url, parameters, rootElement);

        protected async Task Request(Method method, string url, Dictionary<string, object>? parameters = null) => await Client!.Request(method, url, parameters);

        protected async Task<T> Update<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Request<T>(Method.Patch, url, parameters, rootElement);

        protected async Task UpdateNoResponse(string url, Dictionary<string, object>? parameters = null) => await Request(Method.Patch, url, parameters);
    }
}
