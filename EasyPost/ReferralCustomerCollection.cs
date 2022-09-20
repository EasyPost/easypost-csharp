using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class ReferralCustomerCollection : Resource
    {
        #region JSON Properties

        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("referral_customers")]
        public List<ReferralCustomer> referral_customers { get; set; }

        #endregion
    }
}
