using System;
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
    ///     Class representing a set of rate-related functionality.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RateService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RateService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal RateService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Retrieve a Rate from its id.
        /// </summary>
        /// <param name="id">String representing a rate. Starts with `rate_`.</param>
        /// <returns>EasyPost.Rate instance.</returns>
        [CrudOperations.Read]
        public async Task<Rate> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Rate>(Method.Get, $"rates/{id}", cancellationToken);

        #endregion

        /// <summary>
        ///     Get the lowest rate from a list of rates.
        ///
        ///     Deprecated. Use <see cref="EasyPost.Utilities.Rates.GetLowestRate(IEnumerable{EasyPost.Models.API.Rate}, List{string}?, List{string}?, List{string}?, List{string}?)"/> instead.
        /// </summary>
        /// <param name="rates">List of rates to filter.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.Rate object instance.</returns>
        [Obsolete("This method is deprecated. Please use EasyPost.Utilities.Rates.GetLowestRate() instead. This method will be removed in a future version.", false)]
#pragma warning disable CA1822
        public Rate GetLowestRate(IEnumerable<Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null) => Utilities.Rates.GetLowestRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
#pragma warning restore CA1822
    }
}
