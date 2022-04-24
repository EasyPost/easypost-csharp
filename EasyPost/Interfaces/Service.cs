using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Http;
using RestSharp;

namespace EasyPost.Interfaces
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
            var resource = await Client.Execute<T>(request);
            ((resource as Resource)!).Client = Client;
            return resource;
        }

        protected async Task<bool> CreateBlind(string url, Dictionary<string, object>? parameters = null)
        {
            Request request = new Request(url, Method.Post, parameters);
            return await Client.Execute(request);
        }

        protected async Task<T> Get<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            Request request = new Request(url, Method.Get, parameters);
            var resource = await Client.Execute<T>(request);
            ((resource as Resource)!).Client = Client;
            return resource;
        }

        protected async Task<T> List<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            Request request = new Request(url, Method.Get, parameters);
            var resource = await Client.Execute<T>(request);
            ((resource as Resource)!).Client = Client;
            return resource;
        }

        protected async Task<T> Update<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            Request request = new Request(url, Method.Put, parameters);
            var resource = await Client.Execute<T>(request);
            ((resource as Resource)!).Client = Client;
            return resource;
        }

        protected async Task<bool> UpdateBlind(string url, Dictionary<string, object>? parameters = null)
        {
            Request request = new Request(url, Method.Put, parameters);
            return await Client.Execute(request);
        }

        protected async Task<T> Delete<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            Request request = new Request(url, Method.Delete, parameters);
            var resource = await Client.Execute<T>(request);
            ((resource as Resource)!).Client = Client;
            return resource;
        }

        protected async Task<bool> DeleteBlind(string url, Dictionary<string, object>? parameters = null)
        {
            Request request = new Request(url, Method.Delete, parameters);
            return await Client.Execute(request);
        }
    }
}
