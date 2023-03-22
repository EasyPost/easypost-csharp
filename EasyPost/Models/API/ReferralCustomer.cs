using System.Collections.Generic;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ReferralCustomer : BaseUser, IReferralCustomerParameter
    {
        internal ReferralCustomer()
        {
        }
    }

    public class ReferralCustomerCollection : PaginatedCollection<ReferralCustomer>
    {
        #region JSON Properties

        [JsonProperty("referral_customers")]
        public List<ReferralCustomer>? ReferralCustomers { get; set; }

        #endregion

        internal ReferralCustomerCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<ReferralCustomer> entries, int? pageSize = null) => throw new System.NotImplementedException();
    }
}
