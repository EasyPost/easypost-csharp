using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Utilities;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Shipment : Resource
    {
        #region JSON Properties

        [JsonProperty("batch_id")]
        public string batch_id { get; set; }
        [JsonProperty("batch_message")]
        public string batch_message { get; set; }
        [JsonProperty("batch_status")]
        public string batch_status { get; set; }
        [JsonProperty("buyer_address")]
        public Address buyer_address { get; set; }
        [JsonProperty("carrier_accounts")]
        public List<CarrierAccount> carrier_accounts { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("customs_info")]
        public CustomsInfo customs_info { get; set; }
        [JsonProperty("fees")]
        public List<Fee> fees { get; set; }
        [JsonProperty("forms")]
        public List<Form> forms { get; set; }
        [JsonProperty("from_address")]
        public Address from_address { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("insurance")]
        public string insurance { get; set; }
        [JsonProperty("is_return")]
        public bool? is_return { get; set; }
        [JsonProperty("messages")]
        public List<Message> messages { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("options")]
        public Options options { get; set; }
        [JsonProperty("order_id")]
        public string order_id { get; set; }
        [JsonProperty("parcel")]
        public Parcel parcel { get; set; }
        [JsonProperty("postage_label")]
        public PostageLabel postage_label { get; set; }
        [JsonProperty("rates")]
        public List<Rate> rates { get; set; }
        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("refund_status")]
        public string refund_status { get; set; }
        [JsonProperty("return_address")]
        public Address return_address { get; set; }
        [JsonProperty("scan_form")]
        public ScanForm scan_form { get; set; }
        [JsonProperty("selected_rate")]
        public Rate selected_rate { get; set; }
        [JsonProperty("service")]
        public string service { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("tax_identifiers")]
        public List<TaxIdentifier> tax_identifiers { get; set; }
        [JsonProperty("to_address")]
        public Address to_address { get; set; }
        [JsonProperty("tracker")]
        public Tracker tracker { get; set; }
        [JsonProperty("tracking_code")]
        public string tracking_code { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("usps_zone")]
        public string usps_zone { get; set; }

        #endregion

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rateId">The id of the rate to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the shipment for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        /// <param name="endShipperId">The id of the end shipper to use for this purchase.</param>
        [ExcludeFromCodeCoverage]
        public async Task Buy(string rateId, string? insuranceValue = null, bool withCarbonOffset = false, string? endShipperId = null)
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Request request = new Request("shipments/{id}/buy", Method.Post);
            request.AddUrlSegment("id", id);

            Dictionary<string, object> body =
                new Dictionary<string, object>
                {
                    {
                        "rate", new Dictionary<string, object>
                        {
                            {
                                "id", rateId
                            }
                        }
                    },
                    {
                        "carbon_offset", withCarbonOffset
                    }
                };

            if (insuranceValue != null)
            {
                body["insurance"] = insuranceValue;
            }

            if (endShipperId != null)
            {
                body["end_shipper_id"] = endShipperId;
            }

            request.AddParameters(body);

            Shipment result = await request.Execute<Shipment>();

            insurance = result.insurance;
            postage_label = result.postage_label;
            tracking_code = result.tracking_code;
            tracker = result.tracker;
            selected_rate = result.selected_rate;
            forms = result.forms;
            messages = result.messages;
            fees = result.fees;
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object instance to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the shipment for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        /// <param name="endShipperId">The id of the end shipper to use for this purchase.</param>
        [ExcludeFromCodeCoverage]
        public async Task Buy(Rate rate, string? insuranceValue = null, bool withCarbonOffset = false, string? endShipperId = null) => await Buy(rate.id, insuranceValue, withCarbonOffset, endShipperId);

        /// <summary>
        ///     Generate a form for the shipment.
        /// </summary>
        /// <param name="formType">type of the form.</param>
        /// <param name="formOptions">options of the form.</param>
        /// <returns>An EasyPost.Shipment instance.</returns>
        public async Task GenerateForm(string formType, Dictionary<string, object>? formOptions = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {
                    "type", formType
                },
            };

            formOptions?.ToList().ForEach(option => parameters.Add(option.Key, option.Value));

            Dictionary<string, object> wrappedParameters = new Dictionary<string, object>
            {
                {
                    "form", parameters
                }
            };

            Request request = new Request("shipments/{id}/forms", Method.Post);
            request.AddParameters(wrappedParameters);
            request.AddUrlSegment("id", id);

            Merge(await request.Execute<Shipment>());
        }

        /// <summary>
        ///     Generate a postage label for this shipment.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        public async Task GenerateLabel(string fileFormat)
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Request request = new Request("shipments/{id}/label", Method.Get);
            request.AddUrlSegment("id", id);
            request.AddParameter("file_format", fileFormat);

            Merge(await request.Execute<Shipment>());
        }

        /// <summary>
        ///     Get the Smartrates for this shipment.
        /// </summary>
        /// <returns>A list of EasyPost.Smartrate instances.</returns>
        public async Task<List<Smartrate>> GetSmartrates()
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Request request = new Request("shipments/{id}/smartrate", Method.Get);
            request.AddUrlSegment("id", id);
            request.RootElement = "result";
            List<Smartrate> smartrates = await request.Execute<List<Smartrate>>();
            return smartrates;
        }

        /// <summary>
        ///     Insure shipment for the given amount.
        /// </summary>
        /// <param name="amount">The amount to insure the shipment for. Currency is provided when creating a shipment.</param>
        public async Task Insure(double amount)
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Request request = new Request("shipments/{id}/insure", Method.Post);
            request.AddUrlSegment("id", id);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "amount", amount
                }
            });

            Merge(await request.Execute<Shipment>());
        }

        /// <summary>
        ///     Get the lowest rate for this Shipment.
        /// </summary>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.Rate object instance.</returns>
        public Rate LowestRate(List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            return Rates.GetLowestObjectRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }

        /// <summary>
        ///     Get the lowest smartrate for this Shipment.
        /// </summary>
        /// <param name="deliveryDays">Delivery days restriction to use when filtering.</param>
        /// <param name="deliveryAccuracy">Delivery days accuracy restriction to use when filtering.</param>
        /// <returns>Lowest EasyPost.Smartrate object instance.</returns>
        public async Task<Smartrate> LowestSmartrate(int deliveryDays, SmartrateAccuracy deliveryAccuracy)
        {
            List<Smartrate> smartrates = await GetSmartrates();
            return GetLowestSmartrate(smartrates, deliveryDays, deliveryAccuracy);
        }

        /// <summary>
        ///     Send a refund request to the carrier the shipment was purchased from.
        /// </summary>
        public async Task Refund()
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Request request = new Request("shipments/{id}/refund", Method.Get);
            request.AddUrlSegment("id", id);

            Merge(await request.Execute<Shipment>());
        }

        /// <summary>
        ///     Refresh the rates for this Shipment.
        /// </summary>
        /// <param name="parameters">Optional dictionary of parameters for the API request.</param>
        /// <param name="withCarbonOffset">Whether to use carbon offset when re-rating the shipment.</param>
        public async Task RegenerateRates(Dictionary<string, object>? parameters = null, bool withCarbonOffset = false)
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            parameters ??= new Dictionary<string, object>();
            parameters.Add("carbon_offset", withCarbonOffset);

            Request request = new Request("shipments/{id}/rerate", Method.Post);
            request.AddParameters(parameters);
            request.AddUrlSegment("id", id);

            rates = (await request.Execute<Shipment>()).rates;
        }


        /// <summary>
        ///     Get a paginated list of shipments.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing a Shipment. Starts with "shp_". Only retrieve shipments created before
        ///     this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing a Shipment. Starts with "shp_". Only retrieve shipments created after
        ///     this id.
        ///     * {"start_datetime", DateTime} Starting time for the search.
        ///     * {"end_datetime", DateTime} Ending time for the search.
        ///     * {"page_size", int} Size of page. Default to 20.
        ///     * {"purchased", bool} If true only display purchased shipments.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.ShipmentCollection instance.</returns>
        public static async Task<ShipmentCollection> All(Dictionary<string, object>? parameters = null)
        {
            Request request = new Request("shipments", Method.Get);
            request.AddParameters(parameters);

            ShipmentCollection shipmentCollection = await request.Execute<ShipmentCollection>();
            shipmentCollection.filters = parameters;
            return shipmentCollection;
        }

        /// <summary>
        ///     Create a Shipment.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the shipment with. Valid pairs:
        ///     * {"from_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"to_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"buyer_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"return_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"parcel", Dictionary&lt;string, object&gt;} See Parcel.Create for list of valid keys.
        ///     * {"customs_info", Dictionary&lt;string, object&gt;} See CustomsInfo.Create for lsit of valid keys.
        ///     * {"options", Dictionary&lt;string, object&gt;} See https://www.easypost.com/docs/api#shipments for list of
        ///     options.
        ///     * {"is_return", bool}
        ///     * {"currency", string} Defaults to "USD".
        ///     * {"reference", string}
        ///     * {"carrier_accounts", List&lt;string&gt;} List of CarrierAccount.id to limit rating.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <param name="withCarbonOffset">Whether to use carbon offset when creating the shipment.</param>
        /// <returns>An EasyPost.Shipment instance.</returns>
        public static async Task<Shipment> Create(Dictionary<string, object>? parameters = null, bool withCarbonOffset = false)
        {
            Request request = new Request("shipments", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "shipment", parameters ?? new Dictionary<string, object>()
                },
                {
                    "carbon_offset", withCarbonOffset
                }
            });

            return await request.Execute<Shipment>();
        }

        /// <summary>
        ///     Get the lowest smartrate from a list of smartrates.
        /// </summary>
        /// <param name="smartrates">List of smartrates to filter.</param>
        /// <param name="deliveryDays">Delivery days restriction to use when filtering.</param>
        /// <param name="deliveryAccuracy">Delivery days accuracy restriction to use when filtering.</param>
        /// <returns>Lowest EasyPost.Smartrate object instance.</returns>
        public static Smartrate GetLowestSmartrate(List<Smartrate> smartrates, int deliveryDays, SmartrateAccuracy deliveryAccuracy)
        {
            return Rates.GetLowestShipmentSmartrate(smartrates, deliveryDays, deliveryAccuracy);
        }

        /// <summary>
        ///     Retrieve a Shipment from its id.
        /// </summary>
        /// <param name="id">String representing a Shipment. Starts with "shp_".</param>
        /// <returns>An EasyPost.Shipment instance.</returns>
        public static async Task<Shipment> Retrieve(string id)
        {
            Request request = new Request("shipments/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<Shipment>();
        }
    }
}
