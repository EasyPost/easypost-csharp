using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://docs.easypost.com/docs/carrier-metadata">carrier metadata-related beta functionality</a>.
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
        ///     <a href="https://docs.easypost.com/docs/carrier-metadata#retrieve-carrier-metadata">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters"><see cref="Parameters.CarrierMetadata.Retrieve"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="Carrier"/> objects.</returns>
        [CrudOperations.Read]
        public async Task<List<Carrier>> Retrieve(Parameters.CarrierMetadata.Retrieve? parameters = null, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> data = parameters?.ToDictionary() ?? new Dictionary<string, object>();

            return await RequestAsync<List<Carrier>>(Method.Get, "metadata/carriers", cancellationToken, data, "carriers");
        }

        #endregion
    }
}
