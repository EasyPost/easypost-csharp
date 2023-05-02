using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ApiKeyService : EasyPostService
    {
        internal ApiKeyService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Get a list of all API keys.
        /// </summary>
        /// <returns>An EasyPost.ApiKeyCollection instances.</returns>
        [CrudOperations.Read]
        public async Task<ApiKeyCollection> All() => await RequestAsync<ApiKeyCollection>(Method.Get, "api_keys");

        #endregion
    }
}
