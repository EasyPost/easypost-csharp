using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
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

        internal Order()
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Purchase the shipments within this order with a carrier and service.
        /// </summary>
        /// <param name="withCarrier">The carrier to purchase a shipment from.</param>
        /// <param name="withService">The service to purchase.</param>
        /// <returns>The updated Order.</returns>
        [CrudOperations.Update]
        public async Task<Order> Buy(string withCarrier, string withService)
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            Dictionary<string, object> parameters = new()
            {
                { "carrier", withCarrier },
                { "service", withService },
            };

            await Update<Order>(Method.Post, $"orders/{Id}/buy", parameters);
            return this;
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object instance to purchase the shipment with.</param>
        /// <returns>The updated Order.</returns>
        [CrudOperations.Update]
        public async Task<Order> Buy(Rate rate)
        {
            if (rate.Carrier == null)
            {
                throw new MissingPropertyError(rate, nameof(rate.Carrier));
            }

#pragma warning disable IDE0046
            if (rate.Service == null)
#pragma warning restore IDE0046
            {
                throw new MissingPropertyError(rate, nameof(rate.Service));
            }

            return await Buy(rate.Carrier, rate.Service);
        }

        /// <summary>
        ///     Populate the rates property for this Order.
        /// </summary>
        /// <returns>The task to refresh this Order's rates.</returns>
        [CrudOperations.Update]
        public async Task GetRates()
        {
            // TODO: Should this return the updated Order object?
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            await Update<Order>(Method.Get, $"orders/{Id}/rates");
        }

        #endregion

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
