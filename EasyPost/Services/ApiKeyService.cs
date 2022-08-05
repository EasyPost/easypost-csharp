using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;

namespace EasyPost.Services
{
    public class ApiKeyService : EasyPostService
    {
        internal ApiKeyService(EasyPostClient client) : base(client)
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
