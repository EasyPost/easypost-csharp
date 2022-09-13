using EasyPost.Tests.FixtureData.Components;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData
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

        [JsonProperty("webhook_url")]
        public string WebhookUrl { get; set; }

        #endregion
    }
}
