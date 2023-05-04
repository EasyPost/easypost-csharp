using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Order : EasyPostObject, IOrderParameter
    {
        #region JSON Properties

        [JsonProperty("buyer_address")]
        public Address? BuyerAddress { get; internal set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; internal set; }
        [JsonProperty("customs_info")]
        public CustomsInfo? CustomsInfo { get; internal set; }
        [JsonProperty("from_address")]
        public Address? FromAddress { get; internal set; }
        [JsonProperty("is_return")]
        public bool? IsReturn { get; internal set; }
        [JsonProperty("messages")]
        public List<Message>? Messages { get; internal set; }
        [JsonProperty("rates")]
        public List<Rate>? Rates { get; internal set; }
        [JsonProperty("reference")]
        public string? Reference { get; internal set; }
        [JsonProperty("return_address")]
        public Address? ReturnAddress { get; internal set; }
        [JsonProperty("service")]
        public string? Service { get; internal set; }
        [JsonProperty("shipments")]
        public List<Shipment>? Shipments { get; internal set; }
        [JsonProperty("to_address")]
        public Address? ToAddress { get; internal set; }

        #endregion

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
