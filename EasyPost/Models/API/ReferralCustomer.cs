using System.Collections.Generic;
using System.Linq;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.ReferralCustomer
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#referral-customers">EasyPost referral customer</a>.
    /// </summary>
    public class ReferralCustomer : BaseUser, Parameters.IReferralCustomerParameter
    {
    }
#pragma warning disable CA1724 // Naming conflicts with Parameters.ReferralCustomer

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="ReferralCustomer"/>s.
    /// </summary>
    public class ReferralCustomerCollection : PaginatedCollection<ReferralCustomer>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="ReferralCustomer"/>s in the collection.
        /// </summary>
        [JsonProperty("referral_customers")]
        public List<ReferralCustomer>? ReferralCustomers { get; set; }

        #endregion

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<ReferralCustomer> entries, int? pageSize = null)
        {
            Parameters.ReferralCustomer.All parameters = Filters != null ? (Parameters.ReferralCustomer.All)Filters : new Parameters.ReferralCustomer.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
