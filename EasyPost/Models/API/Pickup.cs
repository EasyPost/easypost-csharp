using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Pickup
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/pickups#pickup-object">EasyPost pickup</a>.
    /// </summary>
    public class Pickup : EasyPostObject, Parameters.IPickupParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="EasyPost.Models.API.Address"/> associated with this Pickup.
        /// </summary>
        [JsonProperty("address")]
        public Address? Address { get; set; }

        /// <summary>
        ///     The list of <see cref="EasyPost.Models.API.CarrierAccount"/>s used to generate rates for this Pickup.
        ///     If empty, all of the account's carrier accounts will be used.
        /// </summary>
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; set; }

        /// <summary>
        ///     The confirmation number for this booked Pickup.
        /// </summary>
        [JsonProperty("confirmation")]
        public string? Confirmation { get; set; }

        /// <summary>
        ///     Additional text to help the driver successfully obtain the package.
        /// </summary>
        [JsonProperty("instructions")]
        public string? Instructions { get; set; }

        /// <summary>
        ///     Whether the <see cref="Address"/> is the account address.
        /// </summary>
        [JsonProperty("is_account_address")]
        public bool? IsAccountAddress { get; set; }

        /// <summary>
        ///     The latest time at which the package is available to pick up.
        ///     Must be later than <see cref="MinDatetime"/>.
        /// </summary>
        [JsonProperty("max_datetime")]
        public DateTime? MaxDatetime { get; set; }

        /// <summary>
        ///     A list of messages containing carrier errors encountered during pickup rate generation.
        /// </summary>
        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }

        /// <summary>
        ///     The earliest time at which the package is available to pick up.
        ///     Must be earlier than <see cref="MaxDatetime"/>.
        /// </summary>
        [JsonProperty("min_datetime")]
        public DateTime? MinDatetime { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     A list of different pickup rates across valid carrier accounts for the shipment.
        /// </summary>
        [JsonProperty("pickup_rates")]
        public List<PickupRate>? PickupRates { get; set; }

        /// <summary>
        ///     An optional field that may be used in place of ID in some API endpoints.
        /// </summary>
        [JsonProperty("reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     Current status of the Pickup. One of:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"scheduled"</description>
        ///         </item>
        ///         <item>
        ///             <description>"canceled"</description>
        ///         </item>
        ///         <item>
        ///             <description>"unknown"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        #endregion

        /// <summary>
        ///     Gets the pickup rates as a list of <see cref="Rate"/> objects.
        /// </summary>
        /// <returns>List of <see cref="Rate"/> objects.</returns>
        // ReSharper disable once UseCollectionExpression
        private IEnumerable<Rate> Rates => PickupRates != null ? PickupRates.Cast<Rate>().ToList() : new List<Rate>();

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
#pragma warning restore CA1724 // Naming conflicts with Parameters.Pickup

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
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Pickup> entries, int? pageSize = null)
        {
            Parameters.Pickup.All parameters = Filters != null ? (Parameters.Pickup.All)Filters : new Parameters.Pickup.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
