using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ReferralCustomerCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("referral_customers")]
        public List<ReferralCustomer>? ReferralCustomers { get; set; }

        #endregion

        internal ReferralCustomerCollection()
        {
        }
    }
}
