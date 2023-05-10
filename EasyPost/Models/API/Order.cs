using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Order
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#order-object">EasyPost order</a>.
    /// </summary>
    public class Order : EasyPostObject, IOrderParameter
    {
        #region JSON Properties

        [JsonProperty("buyer_address")]
        public Address? BuyerAddress { get; set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; set; }
        [JsonProperty("customs_info")]
        public CustomsInfo? CustomsInfo { get; set; }
        [JsonProperty("from_address")]
        public Address? FromAddress { get; set; }
        [JsonProperty("is_return")]
        public bool? IsReturn { get; set; }
        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }
        [JsonProperty("rates")]
        public List<Rate>? Rates { get; set; }

        /// <summary>
        ///     An optional field that may be used in place of ID in some API endpoints.
        /// </summary>
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("return_address")]
        public Address? ReturnAddress { get; set; }
        [JsonProperty("service")]
        public string? Service { get; set; }
        [JsonProperty("shipments")]
        public List<Shipment>? Shipments { get; set; }
        [JsonProperty("to_address")]
        public Address? ToAddress { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        internal Order()
        {
        }

        /// <summary>
        ///     Get the lowest rate for this Order.
        /// </summary>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.Rate object instance.</returns>
        public Rate LowestRate(List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
#pragma warning disable IDE0046
            if (Rates == null)
#pragma warning restore IDE0046
            {
                throw new MissingPropertyError(this, nameof(Rates));
            }

            return Utilities.Rates.GetLowestRate(Rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }
    }
}
#pragma warning disable CA1724 // Naming conflicts with Parameters.Order
