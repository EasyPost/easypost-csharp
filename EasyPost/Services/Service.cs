using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost.Services
{
    public class Service
    {
        protected readonly ApiClient Client;

        protected Service(ApiClient client)
        {
            Client = client;
        }

        protected async Task<T> Create<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            Request request = new Request(url, Method.Post, parameters);
            return await Client.Execute<T>(request);
        }

        protected async Task<T> Get<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            Request request = new Request(url, Method.Get, parameters);
            return await Client.Execute<T>(request);
        }

        protected async Task<T> List<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            Request request = new Request(url, Method.Get, parameters);
            return await Client.Execute<T>(request);
        }

        protected async Task<T> Update<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            Request request = new Request(url, Method.Put, parameters);
            return await Client.Execute<T>(request);
        }

        protected async Task<T> Delete<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            Request request = new Request(url, Method.Delete, parameters);
            return await Client.Execute<T>(request);
        }
    }
}
