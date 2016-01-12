using RestSharp;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class Item : IResource {
        public string id { get; set; }
        public Nullable<DateTime> created_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
        public string mode { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string reference { get; set; }
        public string harmonized_code { get; set; }
        public string country_of_origin { get; set; }
        public string warehouse_location { get; set; }
        public double value { get; set; }
        public double length { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        // ADD CUSTOM REFERENCES (e.g. sku, upc)

        /// <summary>
        /// Retrieve an Item from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Item. Starts with "item_" if passing an id.</param>
        /// <returns>EasyPost.Item instance.</returns>
        public static Item Retrieve(string id) {
            Request request = new Request("items/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Item>();
        }

        /// <summary>
        /// Create an Item.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the item with. Valid pairs:
        ///   * {"name", string}
        ///   * {"description", string}
        ///   * {"reference", string}
        ///   * {"harmonized_code", string}
        ///   * {"country_of_origin", string}
        ///   * {"warehouse_location", string}
        ///   * {"value", double}
        ///   * {"length", double}
        ///   * {"width", double}
        ///   * {"height", double}
        ///   * {"weight", double}
        ///   ADD ANY CUSTOM REFERENCES HERE
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Item instance.</returns>
        public static Item Create(Dictionary<string, object> parameters) {
            Request request = new Request("items", Method.POST);
            request.AddBody(parameters, "item");

            return request.Execute<Item>();
        }

        /// <summary>
        /// Retrieve a Item from a custom reference.
        /// </summary>
        /// <param name="name">String containing the name of the custom reference to search for.</param>
        /// <param name="value">String containing the value of the custom reference to search for.</param>
        /// <returns>EasyPost.Item instance.</returns>
        public static Item RetrieveReference(string name, string value) {
            Request request = new Request("items/retrieve_reference/?{name}={value}");
            request.AddUrlSegment("name", name);
            request.AddUrlSegment("value", value);

            return request.Execute<Item>();
        }
    }
}
