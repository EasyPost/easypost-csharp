using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class User : Resource
    {
        [JsonProperty("api_keys")]
        public List<ApiKey> api_keys { get; set; }
        [JsonProperty("balance")]
        public string balance { get; set; }
        [JsonProperty("children")]
        public List<User> children { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("parent_id")]
        public string parent_id { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
        [JsonProperty("password_confirmation")]
        public string password_confirmation { get; set; }
        [JsonProperty("phone_number")]
        public string phone_number { get; set; }
        [JsonProperty("price_per_shipment")]
        public string price_per_shipment { get; set; }
        [JsonProperty("recharge_amount")]
        public string recharge_amount { get; set; }
        [JsonProperty("recharge_threshold")]
        public string recharge_threshold { get; set; }
        [JsonProperty("secondary_recharge_amount")]
        public string secondary_recharge_amount { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Delete the user.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task<bool> Delete()
        {
            Request request = new Request("users/{id}", Method.Delete);
            request.AddUrlSegment("id", id);
            return await request.Execute();
        }

        /// <summary>
        ///     Update the User associated with the api_key specified.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"name", string} Name on the account.
        ///     * {"email", string} Email on the account. Can only be updated on the parent account.
        ///     * {"phone_number", string} Phone number on the account. Can only be updated on the parent account.
        ///     * {"recharge_amount", int} Recharge amount for the account in cents. Can only be updated on the parent account.
        ///     * {"secondary_recharge_amount", int} Secondary recharge amount for the account in cents. Can only be updated on the
        ///     parent account.
        ///     * {"recharge_threshold", int} Recharge threshold for the account in cents. Can only be updated on the parent
        ///     account.
        ///     All invalid keys will be ignored.
        /// </param>
        public async Task Update(Dictionary<string, object> parameters)
        {
            Request request = new Request("users/{id}", Method.Put);
            request.AddUrlSegment("id", id);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "user", parameters
                }
            });

            Merge(await request.Execute<User>());
        }

        /// <summary>
        ///     Update the User's brand.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to update the brand with. Valid pairs:
        ///     * {"ad", string} Base64 encoded string for a png, gif, jpeg, or svg.
        ///     * {"ad_href", string} Valid URL under 255 characters
        ///     * {"background_color", string} Valid hex code
        ///     * {"color", string} Valid hex code
        ///     * {"logo", string} Base64 encoded string for a png, gif, jpeg, or svg
        ///     * {"logo_href", string} Valid URL under 255 characters
        ///     * {"theme", string} "theme1" or "theme2"
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Brand instance.</returns>
        public async Task<Brand> UpdateBrand(Dictionary<string, object> parameters)
        {
            Dictionary<string, object> wrappedParameters = new Dictionary<string, object>
            {
                {
                    "brand", parameters
                }
            };

            Request request = new Request("users/{id}/brand", Method.Put);
            request.AddUrlSegment("id", id);
            request.AddBody(wrappedParameters);

            return await request.Execute<Brand>();
        }

        /// <summary>
        ///     Create a child user for the account associated with the api_key specified.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"name", string} Name on the account.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.User instance.</returns>
        public static async Task<User> Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("users", Method.Post);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "user", parameters
                }
            });

            return await request.Execute<User>();
        }


        /// <summary>
        ///     Retrieve a User from its id. If no id is specified, it returns the user for the api_key specified.
        /// </summary>
        /// <param name="id">String representing a user. Starts with "user_".</param>
        /// <returns>EasyPost.User instance.</returns>
        public static async Task<User> Retrieve(string? id = null)
        {
            Request request;

            if (id == null)
            {
                request = new Request("users");
            }
            else
            {
                request = new Request("users/{id}");
                request.AddUrlSegment("id", id);
            }

            return await request.Execute<User>();
        }


        /// <summary>
        ///     Retrieve the current user.
        /// </summary>
        /// <returns>EasyPost.User instance.</returns>
        public static async Task<User> RetrieveMe()
        {
            Request request;

            request = new Request("users");

            return await request.Execute<User>();
        }
    }
}
