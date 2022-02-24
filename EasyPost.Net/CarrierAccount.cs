using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class CarrierAccount : Resource
    {
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("credentials")]
        public Dictionary<string, object> credentials { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("readable")]
        public string readable { get; set; }
        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("test_credentials")]
        public Dictionary<string, object> test_credentials { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Remove this CarrierAccount from your account.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public bool Destroy()
        {
            Request request = new Request("carrier_accounts/{id}", Method.Delete);
            request.AddUrlSegment("id", id);

            return request.Execute();
        }

        /// <summary>
        ///     Update this CarrierAccount.
        /// </summary>
        /// <param name="parameters">See CarrierAccount.Create for more details.</param>
        public void Update(Dictionary<string, object> parameters)
        {
            Request request = new Request("carrier_accounts/{id}", Method.Put);
            request.AddUrlSegment("id", id);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "carrier_account", parameters
                }
            });

            Merge(request.Execute<CarrierAccount>());
        }

        /// <summary>
        ///     Create a CarrierAccount.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"type", string} Required (e.g. EndiciaAccount, UPSAccount, etc.).
        ///     * {"reference", string} External reference for carrier account.
        ///     * {"description", string} Description of carrier account.
        ///     * {"credentials", Dictionary&lt;string, string&gt;}
        ///     * {"test_credentials", Dictionary&lt;string, string&gt;}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        public static CarrierAccount Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("carrier_accounts", Method.Post);
            request.AddBody(new Dictionary<string, object>
            {
                {
                    "carrier_account", parameters
                }
            });

            return request.Execute<CarrierAccount>();
        }


        /// <summary>
        ///     List all available carrier accounts.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierAccount instances.</returns>
        public static List<CarrierAccount> All()
        {
            Request request = new Request("carrier_accounts");
            return request.Execute<List<CarrierAccount>>();
        }

        /// <summary>
        ///     Retrieve a CarrierAccount from its id.
        /// </summary>
        /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        public static CarrierAccount Retrieve(string id)
        {
            Request request = new Request("carrier_accounts/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<CarrierAccount>();
        }
    }
}
