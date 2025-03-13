using System.Collections.Generic;
using Newtonsoft.Json;

#pragma warning disable CS8618

// ReSharper disable once CheckNamespace
namespace EasyPost.Tests._Utilities
{
    public class FixtureStructure
    {
        #region JSON Properties

        [JsonProperty("addresses")]
        public Addresses Addresses { get; set; }

        [JsonProperty("carrier_accounts")]
        public CarrierAccounts CarrierAccounts { get; set; }

        [JsonProperty("carrier_strings")]
        public CarrierStrings CarrierStrings { get; set; }

        [JsonProperty("claims")]
        public Claims Claims { get; set; }

        [JsonProperty("credit_cards")]
        public CreditCards CreditCards { get; set; }

        [JsonProperty("customs_infos")]
        public CustomsInfos CustomsInfos { get; set; }

        [JsonProperty("customs_items")]
        public CustomsItems CustomsItems { get; set; }

        [JsonProperty("form_options")]
        public FormOptions FormOptions { get; set; }

        [JsonProperty("insurances")]
        public Insurances Insurances { get; set; }

        [JsonProperty("orders")]
        public Orders Orders { get; set; }

        [JsonProperty("page_sizes")]
        public PageSizes PageSizes { get; set; }

        [JsonProperty("parcels")]
        public Parcels Parcels { get; set; }

        [JsonProperty("pickups")]
        public Pickups Pickups { get; set; }

        [JsonProperty("report_types")]
        public ReportTypes ReportTypes { get; set; }

        [JsonProperty("service_names")]
        public ServiceNames ServiceNames { get; set; }

        [JsonProperty("shipments")]
        public Shipments Shipments { get; set; }

        [JsonProperty("tax_identifiers")]
        public TaxIdentifiers TaxIdentifiers { get; set; }

        [JsonProperty("users")]
        public Users Users { get; set; }

        [JsonProperty("webhooks")]
        public Webhooks Webhooks { get; set; }

        #endregion
    }

    public class Addresses
    {
        #region JSON Properties

        [JsonProperty("ca_address_1")]
        public Dictionary<string, object> CaAddress1 { get; set; }

        [JsonProperty("ca_address_2")]
        public Dictionary<string, object> CaAddress2 { get; set; }

        [JsonProperty("incorrect")]
        public Dictionary<string, object> IncorrectAddress { get; set; }

        #endregion
    }

    public class CarrierAccounts
    {
        #region JSON Properties

        [JsonProperty("basic")]
        public Dictionary<string, object> Basic { get; set; }

        #endregion
    }

    public class CarrierStrings
    {
        #region JSON Properties

        [JsonProperty("usps")]
        public string Usps { get; set; }

        #endregion
    }

    public class Claims
    {
        #region JSON Properties

        [JsonProperty("basic")]
        public Dictionary<string, object> Basic { get; set; }

        #endregion
    }

    public class CreditCards
    {
        #region JSON Properties

        [JsonProperty("test")]
        public Dictionary<string, object> Test { get; set; }

        #endregion
    }

    public class CustomsInfos
    {
        #region JSON Properties

        [JsonProperty("basic")]
        public Dictionary<string, object> Basic { get; set; }

        #endregion
    }

    public class CustomsItems
    {
        #region JSON Properties

        [JsonProperty("basic")]
        public Dictionary<string, object> Basic { get; set; }

        #endregion
    }

    public class FormOptions
    {
        #region JSON Properties

        [JsonProperty("rma")]
        public Dictionary<string, object> Rma { get; set; }

        #endregion
    }

    public class Insurances
    {
        #region JSON Properties

        [JsonProperty("basic")]
        public Dictionary<string, object> Basic { get; set; }

        #endregion
    }

    public class Orders
    {
        #region JSON Properties

        [JsonProperty("basic")]
        public Dictionary<string, object> Basic { get; set; }

        #endregion
    }

    public class PageSizes
    {
        #region JSON Properties

        [JsonProperty("fifty_results")]
        public int Fifty { get; set; }

        [JsonProperty("five_results")]
        public int Five { get; set; }

        [JsonProperty("one_result")]
        public int One { get; set; }

        [JsonProperty("one_hundred_results")]
        public int OneHundred { get; set; }

        [JsonProperty("ten_results")]
        public int Ten { get; set; }

        [JsonProperty("twenty_results")]
        public int Twenty { get; set; }

        #endregion
    }

    public class Parcels
    {
        #region JSON Properties

        [JsonProperty("basic")]
        public Dictionary<string, object> Basic { get; set; }

        #endregion
    }

    public class Pickups
    {
        #region JSON Properties

        [JsonProperty("basic")]
        public Dictionary<string, object> Basic { get; set; }

        #endregion
    }

    public class ReportTypes
    {
        #region JSON Properties

        [JsonProperty("shipment")]
        public string Shipment { get; set; }

        #endregion
    }

    public class ServiceNames
    {
        #region JSON Properties

        [JsonProperty("usps")]
        public ServiceNamesUsps Usps { get; set; }

        #endregion
    }

    public class ServiceNamesUsps
    {
        #region JSON Properties

        [JsonProperty("first_service")]
        public string FirstService { get; set; }

        [JsonProperty("pickup_service")]
        public string PickupService { get; set; }

        #endregion
    }

    public class Shipments
    {
        #region JSON Properties

        [JsonProperty("basic_domestic")]
        public Dictionary<string, object> BasicDomestic { get; set; }

        [JsonProperty("full")]
        public Dictionary<string, object> Full { get; set; }

        #endregion
    }

    public class TaxIdentifiers
    {
        #region JSON Properties

        [JsonProperty("basic")]
        public Dictionary<string, object> Basic { get; set; }

        #endregion
    }

    public class Users
    {
        #region JSON Properties

        [JsonProperty("referral")]
        public Dictionary<string, object> Referral { get; set; }

        #endregion
    }

    public class Webhooks
    {
        #region JSON Properties

        [JsonProperty("hmac_signature")]
        public string HmacSignature { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("custom_headers")]
        public List<object> CustomHeaders { get; set; }

        #endregion
    }
}
