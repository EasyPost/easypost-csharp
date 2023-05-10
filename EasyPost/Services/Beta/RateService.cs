using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API.Beta;
using EasyPost.Parameters.Beta.Rates;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services.Beta
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#rates">rate-related beta functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RateService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RateService"/> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal RateService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Retrieve <see cref="Models.API.Beta.StatelessRate"/>s for a <see cref="Models.API.Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-rates-for-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to retrieve the rates with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="StatelessRate"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<List<StatelessRate>> RetrieveStatelessRates(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("shipment");
            return await RequestAsync<List<StatelessRate>>(Method.Post, "rates", cancellationToken, parameters, "rates", ApiVersion.Beta);
        }

        /// <summary>
        ///     Retrieve <see cref="Models.API.Beta.StatelessRate"/>s for a <see cref="Models.API.Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-rates-for-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to retrieve the rates with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="StatelessRate"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<List<StatelessRate>> RetrieveStatelessRates(Retrieve parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<List<StatelessRate>>(Method.Post, "rates", cancellationToken, parameters.ToDictionary(), "rates", ApiVersion.Beta);
        }

        #endregion
    }
}
