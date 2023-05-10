using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using EasyPost.Parameters;
using EasyPost.Parameters.Pickups;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#pickup-object">EasyPost pickup</a>.
    /// </summary>
    public class Pickup : EasyPostObject, IPickupParameter
    {
        #region JSON Properties

        [JsonProperty("address")]
        public Address? Address { get; set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; set; }
        [JsonProperty("confirmation")]
        public string? Confirmation { get; set; }
        [JsonProperty("instructions")]
        public string? Instructions { get; set; }
        [JsonProperty("is_account_address")]
        public bool? IsAccountAddress { get; set; }
        [JsonProperty("max_datetime")]
        public DateTime? MaxDatetime { get; set; }
        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }
        [JsonProperty("min_datetime")]
        public DateTime? MinDatetime { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("pickup_rates")]
        public List<PickupRate>? PickupRates { get; set; }

        /// <summary>
        ///     An optional field that may be used in place of ID in some API endpoints.
        /// </summary>
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }

        #endregion

        /// <summary>
        ///     Gets the pickup rates as a list of Rate objects.
        /// </summary>
        /// <returns>List of Rate objects.</returns>
        private IEnumerable<Rate> Rates => PickupRates != null ? PickupRates.Cast<Rate>().ToList() : new List<Rate>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="Pickup"/> class.
        /// </summary>
        internal Pickup()
        {
        }

        /// <summary>
        ///     Get the lowest rate for this Pickup.
        /// </summary>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.PickupRate object instance.</returns>
        public PickupRate LowestRate(List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null) => (PickupRate)Utilities.Rates.GetLowestRate(Rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
    }

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="Pickup"/> objects.
    /// </summary>
    public class PickupCollection : PaginatedCollection<Pickup>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Pickup"/>s in the collection.
        /// </summary>
        [JsonProperty("pickups")]
        public List<Pickup>? Pickups { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="PickupCollection"/> class.
        /// </summary>
        internal PickupCollection()
        {
        }

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Pickup> entries, int? pageSize = null)
        {
            All parameters = Filters != null ? (All)Filters : new All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
