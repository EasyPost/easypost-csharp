using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Calculation;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    public class RateService : EasyPostService
    {
        internal RateService(Client client) : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Retrieve a Rate from its id.
        /// </summary>
        /// <param name="id">String representing a rate. Starts with `rate_`.</param>
        /// <returns>EasyPost.Rate instance.</returns>
        [CrudOperations.Read]
        public async Task<Rate> Retrieve(string id)
        {
            return await Get<Rate>($"rates/{id}");
        }

        #endregion

        /// <summary>
        ///     Get the lowest rate from a list of rates.
        /// </summary>
        /// <param name="rates">List of rates to filter.</param>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.Rate object instance.</returns>
        public Rate GetLowestRate(IEnumerable<Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            return Rates.GetLowestObjectRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }
    }
}
