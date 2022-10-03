using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ApiKeyService : EasyPostService
    {
        internal ApiKeyService(EasyPostClient client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Get a list of all API keys.
        /// </summary>
        /// <returns>An EasyPost.ApiKeyCollection instances.</returns>
        [CrudOperations.Read]
        public async Task<ApiKeyCollection> All()
        {
            return await List<ApiKeyCollection>("api_keys");
        }

        #endregion
    }
}
