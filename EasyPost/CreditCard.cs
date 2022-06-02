using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyPost
{
    public class CreditCard : Resource
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("disabled_at")]
        public string disabled_at { get; set; }
        [JsonProperty("object")]
        public string Object { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("last4")]
        public string last4 { get; set; }
        [JsonProperty("exp_month")]
        public string exp_month { get; set; }
        [JsonProperty("exp_year")]
        public string exp_year { get; set; }
        [JsonProperty("brand")]
        public string brand { get; set; }

        /// <summary>
        ///     Fund this credit card.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <returns>An EasyPost.CreditCardFund instance.</returns>
        public async Task<CreditCardFund> Fund(string amount)
        {
            return await PaymentMethod.Fund(amount, id);
        }


        /// <summary>
        ///     Delete this credit card.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task<bool> Delete()
        {
            return await PaymentMethod.Delete(id);
        }
    }
}
