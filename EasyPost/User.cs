using RestSharp;

using System;
using System.Collections.Generic;

namespace EasyPost {
    public class User : Resource {
        public string id { get; set; }
        public string parent_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string password { get; set; }
        public string password_confirmation { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string balance { get; set; }
        public string price_per_shipment { get; set; }
        public string recharge_amount { get; set; }
        public string secondary_recharge_amount { get; set; }
        public string recharge_threshold { get; set; }
        public List<User> children { get; set; }
        public List<ApiKey> api_keys { get; set; }

        /// <summary>
        /// Retrieve a User from its id. If no id is specified, it returns the user for the api_key specified.
        /// </summary>
        /// <param name="id">String representing a user. Starts with "user_".</param>
        /// <returns>EasyPost.User instance.</returns>
        public static User Retrieve(string id = null) {
            Request request;

            if (id == null) {
                request = new Request("users");
            } else {
                request = new Request("users/{id}");
                request.AddUrlSegment("id", id);
            }

            return request.Execute<User>();
        }

        /// <summary>
        /// Create a child user for the account associated with the api_key specified.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///   * {"name", string} Name on the account.
        /// All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.User instance.</returns>
        public static User Create(Dictionary<string, object> parameters) {
            Request request = new Request("users", Method.POST);
            request.AddBody(parameters, "user");

            return request.Execute<User>();
        }

        /// <summary>
        /// Update the User associated with the api_key specified.
        /// </summary>
        /// <param name="parameters">
        /// Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///   * {"name", string} Name on the account.
        ///   * {"email", string} Email on the account. Can only be updated on the parent account.
        ///   * {"phone_number", string} Phone number on the account. Can only be updated on the parent account.
        ///   * {"recharge_amount", int} Recharge amount for the account in cents. Can only be updated on the parent account.
        ///   * {"secondary_recharge_amount", int} Secondary recharge amount for the account in cents. Can only be updated on the parent account.
        ///   * {"recharge_threshold", int} Recharge threshold for the account in cents. Can only be updated on the parent account.
        /// All invalid keys will be ignored.
        /// </param>
        public void Update(Dictionary<string, object> parameters) {
            Request request = new Request("users/{id}", Method.PUT);
            request.AddUrlSegment("id", id);
            request.AddBody(parameters, "user");

            Merge(request.Execute<User>());
        }
    }
}