using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    public class ApiKeyService : EasyPostService
    {
        internal ApiKeyService(EasyPostClient client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Get a list of all API keys.
        /// </summary>
        /// <returns>A list of EasyPost.ApiKey instances.</returns>
        [CrudOperations.Read]
        public async Task<List<ApiKey>> All()
        {
            return await List<List<ApiKey>>("api_keys", null, "keys");
        }

        #endregion
    }
}
