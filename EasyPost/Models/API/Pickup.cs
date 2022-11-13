using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API;

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
    ///     Get the pickup rates as a list of Rate objects.
    /// </summary>
    /// <returns>List of Rate objects.</returns>
    private IEnumerable<Rate> Rates => PickupRates != null ? PickupRates.Cast<Rate>().ToList() : new List<Rate>();

    internal Pickup()
    {
    }
}
