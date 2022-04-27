using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost.Interfaces
{
    public class Service
    {
        protected readonly BaseClient Client;

        protected Service(BaseClient client)
        {
            Client = client;
        }

        protected async Task<T> Create<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            return await Client.Request<T>(Method.Post, url, parameters, rootElement);
        }

        protected async Task<bool> CreateBlind(string url, Dictionary<string, object>? parameters = null)
        {
            return await Client.Request(Method.Post, url, parameters);
        }

        protected async Task<T> Delete<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            return await Client.Request<T>(Method.Delete, url, parameters, rootElement);
        }

        protected async Task<bool> DeleteBlind(string url, Dictionary<string, object>? parameters = null)
        {
            return await Client.Request(Method.Delete, url, parameters);
        }

        protected async Task<T> Get<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            return await Client.Request<T>(Method.Get, url, parameters, rootElement);
        }

        protected async Task<T> List<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            return await Client.Request<T>(Method.Get, url, parameters, rootElement);
        }

        protected async Task<T> Update<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : new()
        {
            return await Client.Request<T>(Method.Put, url, parameters, rootElement);
        }

        protected async Task<bool> UpdateBlind(string url, Dictionary<string, object>? parameters = null)
        {
            return await Client.Request(Method.Put, url, parameters);
        }
    }
}
