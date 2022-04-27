using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models;

namespace EasyPost.Services
{
    public class ApiKeyService : Service
    {
        internal ApiKeyService(BaseClient client) : base(client)
        {
        }

        /// <summary>
        ///     Get a list of all API keys.
        /// </summary>
        /// <returns>A list of EasyPost.ApiKey instances.</returns>
        public async Task<List<ApiKey>> All()
        {
            return await List<List<ApiKey>>("api_keys", null, "keys");
        }
    }
}