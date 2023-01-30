﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Pickup : EasyPostObject
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

        internal Pickup()
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Purchase this pickup.
        /// </summary>
        /// <param name="withCarrier">The name of the carrier to purchase with.</param>
        /// <param name="withService">The name of the service to purchase.</param>
        /// <returns>The updated Pickup.</returns>
        [CrudOperations.Update]
        public async Task<Pickup> Buy(string withCarrier, string withService)
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

            await Update<Pickup>(Method.Post, $"pickups/{Id}/buy", parameters);
            return this;
        }

        /// <summary>
        ///     Cancel this pickup.
        /// </summary>
        /// <returns>The updated Pickup.</returns>
        [CrudOperations.Update]
        public async Task<Pickup> Cancel()
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            await Update<Pickup>(Method.Post, $"pickups/{Id}/cancel");
            return this;
        }

        #endregion

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

    public class PickupCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("pickups")]
        public List<Pickup>? Pickups { get; set; }

        #endregion

        internal PickupCollection()
        {
        }
    }
}
