﻿using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    public class Shipment : IResource {
        public string id { get; set; }
        public string mode { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string tracking_code { get; set; }
        public string reference { get; set; }
        public string status { get; set; }
        public bool is_return { get; set; }
        public Dictionary<string, object> options { get; set; }
        public List<Dictionary<string, object>> messages { get; set; }
        public CustomsInfo customs_info { get; set; }
        public Address from_address { get; set; }
        public Address to_address { get; set; }
        public Parcel parcel { get; set; }
        public PostageLabel postage_label { get; set; }
        public List<Rate> rates { get; set; }
        public ScanForm scan_form { get; set; }
        public List<Forms> forms { get; set; }
        public Rate selected_rate { get; set; }
        public Tracker tracker { get; set; }
        public Address buyer_address { get; set; }
        public Address return_address { get; set; }
        public string refund_status { get; set; }
        public string insurance { get; set; }
        public string batch_status { get; set; }
        public string batch_message { get; set; }
        public string usps_zone { get; set; }
        public string stamp_url { get; set; }
        public string barcode_url { get; set; }

        private static Client client = new Client();

        /// <summary>
        /// Retrieve a Shipment from its id.
        /// </summary>
        /// <param name="id">String representing a Shipment. Starts with "shp_".</param>
        /// <returns>EasyPost.Shipment instance.</returns>
        public static Shipment Retrieve(string id) {
            Request request = new Request("shipments/{id}");
            request.AddUrlSegment("id", id);

            return client.Execute<Shipment>(request);
        }

        /// <summary>
        /// Create a Shipment.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///   * {"from_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"to_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"buyer_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"return_address", Dictionary<string, object>} See Address.Create for a list of valid keys.
        ///   * {"parcel", Dictionary<string, object>} See Parcel.Create for list of valid keys.
        ///   * {"customs_info", Dictionary<string, object>} See CustomsInfo.Create for lsit of valid keys.
        ///   * {"options", Dictionary<string, object>} See https://www.easypost.com/docs/api#shipments for list of options.
        ///   * {"is_return", bool}
        ///   * {"currency", string} Defaults to "USD".
        ///   * {"reference", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Batch instance.</returns>
        public static Shipment Create(IDictionary<string, object> parameters = null) {
            return sendCreate(parameters ?? new Dictionary<string, object>());
        }

        /// <summary>
        /// Create this Shipment.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Shipment already has an id.</exception>
        public void Create() {
            if (id != null)
                throw new ResourceAlreadyCreated();
            this.Merge(sendCreate(this.AsDictionary()));
        }

        private static Shipment sendCreate(IDictionary<string, object> parameters) {
            Request request = new Request("shipments", Method.POST);
            request.addBody(parameters, "shipment");

            return client.Execute<Shipment>(request);
        }

        /// <summary>
        /// Populate the rates property for this shipment. 
        /// </summary>
        public void GetRates() {
            if (id == null)
                Create();

            Request request = new Request("shipments/{id}/rates");
            request.AddUrlSegment("id", id);

            rates = client.Execute<Shipment>(request).rates;
        }

        /// <summary>
        /// Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rateId">The id of the rate to purchase the shipment with.</param>
        public void Buy(string rateId) {
            Request request = new Request("shipments/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);
            request.addBody(new Dictionary<string, object>() { { "id", rateId } }, "rate");

            Shipment result = client.Execute<Shipment>(request);

            insurance = result.insurance;
            postage_label = result.postage_label;
            tracking_code = result.tracking_code;
            selected_rate = result.selected_rate;
        }

        /// <summary>
        /// Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">EasyPost.Rate object to puchase the shipment with.</param>
        public void Buy(Rate rate) {
            Buy(rate.id);
        }

        /// <summary>
        /// Insure shipment for the given amount.
        /// </summary>
        /// <param name="amount">The amount to insure the shipment for. Currency is provided when creating a shipment.</param>
        public void Insure(double amount) {
            Request request = new Request("shipments/{id}/insure", Method.POST);
            request.AddUrlSegment("id", id);
            request.addBody(new List<Tuple<string, string>>() {
                new Tuple<string, string>("amount", amount.ToString())
            });

            ResourceExtension.Merge(this, client.Execute<Shipment>(request));
        }

        /// <summary>
        /// Generate a postage label for this shipment.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        public void GenerateLabel(string fileFormat) {
            Request request = new Request("shipments/{id}/label");
            request.AddUrlSegment("id", id);
            // This is a GET, but uses the request body, so use ParameterType.GetOrPost instead.
            request.AddParameter("file_format", fileFormat, ParameterType.GetOrPost);

            ResourceExtension.Merge(this, client.Execute<Shipment>(request));
        }

        /// <summary>
        /// Generate a stamp for this shipment.
        /// </summary>
        public void GenerateStamp() {
            Request request = new Request("shipments/{id}/stamp");
            request.AddUrlSegment("id", id);

            Shipment result = client.Execute<Shipment>(request);
            stamp_url = result.stamp_url;
        }

        /// <summary>
        /// Generate a barcode for this shipment.
        /// </summary>
        public void GenerateBarcode() {
            Request request = new Request("shipments/{id}/barcode");
            request.AddUrlSegment("id", id);

            Shipment result = client.Execute<Shipment>(request);
            barcode_url = result.barcode_url;
        }

        /// <summary>
        /// Send a refund request to the carrier the shipment was purchased from.
        /// </summary>
        public void Refund() {
            Request request = new Request("shipments/{id}/refund");
            request.AddUrlSegment("id", id);

            ResourceExtension.Merge(this, client.Execute<Shipment>(request));
        }

        /// <summary>
        /// Get the lowest rate for the shipment. Optionally whitelist/blacklist carriers and servies from the search.
        /// </summary>
        /// <param name="includeCarriers">Carriers whitelist.</param>
        /// <param name="includeServices">Services whitelist.</param>
        /// <param name="excludeCarriers">Carriers blacklist.</param>
        /// <param name="excludeServices">Services blacklist.</param>
        /// <returns>EasyPost.Rate instance or null if no rate was found.</returns>
        public Rate LowestRate(IEnumerable<string> includeCarriers = null, IEnumerable<string> includeServices = null,
                               IEnumerable<string> excludeCarriers = null, IEnumerable<string> excludeServices = null) {
            if (rates == null)
                GetRates();

            List<Rate> result = new List<Rate>(rates);

            if (includeCarriers != null)
                filterRates(ref result, rate => includeCarriers.Contains(rate.carrier));
            if (includeServices != null)
                filterRates(ref result, rate => includeServices.Contains(rate.service));
            if (excludeCarriers != null)
                filterRates(ref result, rate => !excludeCarriers.Contains(rate.carrier));
            if (excludeServices != null)
                filterRates(ref result, rate => !excludeServices.Contains(rate.service));

            return result.OrderBy(rate => double.Parse(rate.rate)).FirstOrDefault();
        }

        private void filterRates(ref List<Rate> rates, Func<Rate, bool> filter) {
            rates = rates.Where(filter).ToList();
        }
    }
}
