using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.V2
{
    public class CreditCard : Resource
    {
        [JsonProperty("brand")]
        public string brand { get; set; }
        [JsonProperty("disabled_at")]
        public string disabled_at { get; set; }
        [JsonProperty("exp_month")]
        public string exp_month { get; set; }
        [JsonProperty("exp_year")]
        public string exp_year { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("last4")]
        public string last4 { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("object")]
        public string Object { get; set; }

        /// <summary>
        ///     Delete this credit card from your account.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task<bool> Delete() => await Request(Method.Delete, $"credit_cards/{id}");


        /// <summary>
        ///     Fund this credit card.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <returns>An EasyPost.CreditCardFund instance.</returns>
        public async Task<CreditCardFunding> Fund(string amount)
        {
            Dictionary<string, object> requestParameters = new Dictionary<string, object>
            {
                {
                    "amount", amount
                }
            };

            return await Request<CreditCardFunding>(Method.Post, $"credit_cards/{id}/charge", requestParameters);
        }
    }
}
