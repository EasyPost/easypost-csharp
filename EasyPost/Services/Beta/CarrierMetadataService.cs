using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API.Beta;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services.Beta
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#carrier-metadata">carrier metadata-related beta functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CarrierMetadataService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CarrierMetadataService"/> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal CarrierMetadataService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Retrieve metadata about specific carrier(s).
        ///     <a href="https://www.easypost.com/docs/api#retrieve-carrier-metadata">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters"><see cref="Retrieve"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="Carrier"/> objects.</returns>
        [CrudOperations.Read]
        public async Task<List<Carrier>> RetrieveCarrierMetadata(Parameters.Beta.CarrierMetadata.Retrieve? parameters = null, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> data = parameters?.ToDictionary() ?? new Dictionary<string, object>();

            return await RequestAsync<List<Carrier>>(Method.Get, "metadata", cancellationToken, data, "carriers", ApiVersion.Beta);
        }

        #endregion
    }
}
