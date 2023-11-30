using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#api-keys">API key-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    [SuppressMessage("Performance", "CA1863:Use \'CompositeFormat\'")]
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
        ///     Get a list of all <see cref="ApiKey"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-an-api-key">Related API documentation</a>.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ApiKeyCollection"/> object.</returns>
        [CrudOperations.Read]
        public async Task<ApiKeyCollection> All(CancellationToken cancellationToken = default) => await RequestAsync<ApiKeyCollection>(Method.Get, "api_keys", cancellationToken);

        /// <summary>
        ///     Retrieve the <see cref="ApiKey"/>s for a specific <see cref="User"/>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="User"/> to retrieve keys for.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="ApiKey"/>s for the specified <see cref="User"/>.</returns>
        /// <exception cref="FilteringError">Thrown if the specified <see cref="User"/> does not exist.</exception>
        public async Task<List<ApiKey>?> RetrieveApiKeysForUser(string id, CancellationToken cancellationToken = default)
        {
            ApiKeyCollection apiKeyCollection = await All(cancellationToken);

            if (apiKeyCollection.Id == id)
            {
                return apiKeyCollection.Keys;
            }

            if (apiKeyCollection.Children == null)
            {
                throw new FilteringError(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.NoObjectFound, "child"));
            }

            foreach (ApiKeyCollection child in apiKeyCollection.Children.Where(child => child.Id == id))
            {
                return child.Keys;
            }

            throw new FilteringError(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.NoObjectFound, "child"));
        }

        #endregion
    }
}
