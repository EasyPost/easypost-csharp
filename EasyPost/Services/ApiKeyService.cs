using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#api-keys">API key-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ApiKeyService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiKeyService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
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
        public async Task<ApiKeyCollection> All(CancellationToken cancellationToken = default) => await RequestAsync<ApiKeyCollection>(Method.Get, "api_keys", cancellationToken);

        #endregion
    }
}
