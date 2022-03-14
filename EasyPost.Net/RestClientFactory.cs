using System.Collections.Concurrent;
using RestSharp;

namespace EasyPost
{
    public static class RestClientFactory
    {
        /// <summary>
        /// Dictionary of all static client instances based on the client API base URL
        /// </summary>
        private static readonly ConcurrentDictionary<string, RestClient> _clients = new ConcurrentDictionary<string, RestClient>();

        public static RestClient GetClient(
            RestClientOptions options)
        {
            // Build a key to cache the client against which is the base URL we are connecting to
            string cacheKey = options.BaseUrl!.ToString();

            // If we have an existing instance of this client, return it outside of the lock
            if (_clients.TryGetValue(cacheKey, out var client)) {
                return client;
            }

            // Create a new instance, but make sure only one thread can create it
            lock (_clients) {
                // Try again to get it, as it might have just been created by another thread
                if (_clients.TryGetValue(cacheKey, out client)) {
                    return client;
                }

                client = new RestClient(cacheKey);
                _clients.TryAdd(cacheKey, client);
                return client;
            }
        }
    }
}
