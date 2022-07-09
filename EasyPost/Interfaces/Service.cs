using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using RestSharp;

namespace EasyPost.Interfaces
{
    public abstract class Service : WithClient, IService
    {
        internal Service(Client client)
        {
            Client = client;
        }

        protected async Task<T> Create<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Request<T>(Method.Post, url, parameters, rootElement);

        protected async Task<bool> CreateBlind(string url, Dictionary<string, object>? parameters = null) => await Request(Method.Post, url, parameters);

        protected async Task<T> Delete<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Request<T>(Method.Delete, url, parameters, rootElement);

        protected async Task<bool> DeleteBlind(string url, Dictionary<string, object>? parameters = null) => await Request(Method.Delete, url, parameters);

        protected async Task<T> Get<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Request<T>(Method.Get, url, parameters, rootElement);

        protected async Task<T> List<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Request<T>(Method.Get, url, parameters, rootElement);

        protected async Task<T> Request<T>(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Client.Request<T>(method, url, parameters, rootElement);

        protected async Task<bool> Request(Method method, string url, Dictionary<string, object>? parameters = null, string? rootElement = null) => await Client.Request(method, url, parameters, rootElement);

        protected async Task<T> Update<T>(string url, Dictionary<string, object>? parameters = null, string? rootElement = null) where T : class, new() => await Request<T>(Method.Patch, url, parameters, rootElement);

        protected async Task<bool> UpdateBlind(string url, Dictionary<string, object>? parameters = null) => await Request(Method.Patch, url, parameters);
    }

    public interface IService
    {
    }
}
