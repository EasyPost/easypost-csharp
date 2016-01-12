using RestSharp;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class CustomsItem : IResource {
        public string id { get; set; }
        public Nullable<DateTime> created_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
        public string description { get; set; }
        public string hs_tariff_number { get; set; }
        public string origin_country { get; set; }
        public int quantity { get; set; }
        public double value { get; set; }
        public double weight { get; set; }
        public string mode { get; set; }
        public string currency { get; set; }

        /// <summary>
        /// Retrieve a CustomsItem from its id.
        /// </summary>
        /// <param name="id">String representing a CustomsItem. Starts with "cstitem_".</param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        public static CustomsItem Retrieve(string id) {
            Request request = new Request("customs_items/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<CustomsItem>();
        }

        /// <summary>
        /// Create a CustomsItem.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the customs item with. Valid pairs:
        ///   * {"description", string}
        ///   * {"quantity", int}
        ///   * {"weight", int}
        ///   * {"value", double}
        ///   * {"hs_tariff_number", string}
        ///   * {"origin_country", string}
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CustomsItem instance.</returns>
        public static CustomsItem Create(Dictionary<string, object> parameters) {
            Request request = new Request("customs_items", Method.POST);
            request.AddBody(parameters, "customs_item");

            return request.Execute<CustomsItem>();
        }
    }
}
