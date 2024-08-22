using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Exceptions.General;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Order
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/orders#order-object">EasyPost order</a>.
    /// </summary>
    public class Order : EasyPostObject, Parameters.IOrderParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Address"/> object representing the buyer's address.
        ///     Defaults to <see cref="ToAddress"/>.
        /// </summary>
        [JsonProperty("buyer_address")]
        public Address? BuyerAddress { get; set; }

        /// <summary>
        ///     The <see cref="CarrierAccount"/>s used for this order.
        /// </summary>
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; set; }

        /// <summary>
        ///     The <see cref="CustomsInfo"/> object representing the customs information for this order.
        /// </summary>
        [JsonProperty("customs_info")]
        public CustomsInfo? CustomsInfo { get; set; }

        /// <summary>
        ///     The <see cref="Address"/> object representing the destination address for this order.
        /// </summary>
        [JsonProperty("from_address")]
        public Address? FromAddress { get; set; }

        /// <summary>
        ///     Whether or not this order is a return.
        /// </summary>
        [JsonProperty("is_return")]
        public bool? IsReturn { get; set; }

        /// <summary>
        ///     Any carrier errors encountered while rating the order.
        /// </summary>
        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }

        /// <summary>
        ///     The <see cref="Rate"/>s for the order.
        /// </summary>
        [JsonProperty("rates")]
        public List<Rate>? Rates { get; set; }

        /// <summary>
        ///     An optional field that may be used in place of ID in some API endpoints.
        /// </summary>
        [JsonProperty("reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     The <see cref="Address"/> object representing the return address for the order.
        ///     Defaults to <see cref="FromAddress"/>.
        /// </summary>
        [JsonProperty("return_address")]
        public Address? ReturnAddress { get; set; }

        /// <summary>
        ///     The service level used for the order.
        /// </summary>
        [JsonProperty("service")]
        public string? Service { get; set; }

        /// <summary>
        ///     The <see cref="Shipment"/>s in the order.
        /// </summary>
        [JsonProperty("shipments")]
        public List<Shipment>? Shipments { get; set; }

        /// <summary>
        ///     The <see cref="Address"/> object representing the destination address for this order.
        /// </summary>
        [JsonProperty("to_address")]
        public Address? ToAddress { get; set; }

        #endregion

        /// <summary>
        ///     Get the lowest rate for the order.
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
