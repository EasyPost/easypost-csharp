﻿using RestSharp;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Pickup : Resource {
#pragma warning disable IDE1006 // Naming Styles
        public string id { get; set; }
        public string mode { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public DateTime min_datetime { get; set; }
        public DateTime max_datetime { get; set; }
        public bool is_account_address { get; set; }
        public string instructions { get; set; }
        public List<string> messages { get; set; }
        public string confirmation { get; set; }
        public Address address { get; set; }
        public List<CarrierAccount> carrier_accounts { get; set; }
        public List<Rate> pickup_rates { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static Pickup Retrieve(string id) {
            Request request = new Request("v2/pickups/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Pickup>();
        }

        /// <summary>
        /// Create a Pickup.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///   * {"is_account_address", bool}
        ///   * {"min_datetime", DateTime}
        ///   * {"max_datetime", DateTime}
        ///   * {"reference", string}
        ///   * {"instructions", string}
        ///   * {"carrier_accounts", List&lt;CarrierAccount&gt;}
        ///   * {"address", Address}
        ///   * {"shipment", Shipment}
        ///   * {"batch", Batch}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public static Pickup Create(Dictionary<string, object> parameters = null) {
            return SendCreate(parameters ?? new Dictionary<string, object>());
        }

        /// <summary>
        /// Create this Pickup.
        /// </summary>
        /// <exception cref="ResourceAlreadyCreated">Pickup already has an id.</exception>
        public void Create() {
            if (id != null)
                throw new ResourceAlreadyCreated();
            Merge(SendCreate(this.AsDictionary()));
        }

        private static Pickup SendCreate(Dictionary<string, object> parameters) {
            Request request = new Request("v2/pickups", Method.POST);
            request.AddBody(new Dictionary<string, object>() { { "pickup", parameters } });

            return request.Execute<Pickup>();
        }

        /// <summary>
        /// Purchase this pickup.
        /// </summary>
        /// <param name="carrier">The name of the carrier to purchase with.</param>
        /// <param name="service">The name of the service to purchase.</param>
        public void Buy(string carrier, string service) {
            Request request = new Request("v2/pickups/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);
            request.AddQueryString(new Dictionary<string, object>() { { "carrier", carrier }, { "service", service } });

            Merge(request.Execute<Pickup>());
        }

        /// <summary>
        /// Cancel this pickup.
        /// </summary>
        public void Cancel() {
            Request request = new Request("v2/pickups/{id}/cancel", Method.POST);
            request.AddUrlSegment("id", id);

            Merge(request.Execute<Pickup>());
        }
    }
}
