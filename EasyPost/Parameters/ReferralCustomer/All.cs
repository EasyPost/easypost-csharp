using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters.ReferralCustomer
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/users/referral-customers#retrieve-all-referralcustomers">Parameters</a> for <see cref="EasyPost.Services.ReferralCustomerService.All(All, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class All : BaseAllParameters<Models.API.ReferralCustomer>
    {
        #region Request Parameters

        /// <summary>
        ///     Only records created after the given ID will be included. May not be used with <see cref="BeforeId"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "after_id")]
        public string? AfterId { get; set; }

        /// <summary>
        ///     Only records created before the given ID will be included. May not be used with <see cref="AfterId"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "before_id")]
        public string? BeforeId { get; set; }

        /// <summary>
        ///     The number of records to return on each page. The maximum value is 100, and default is 20.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "page_size")]
        public int? PageSize { get; set; } = 20;

        #endregion

        /// <summary>
        ///     Convert a dictionary into this parameter set.
        /// </summary>
        /// <param name="dictionary">Dictionary to parse.</param>
        /// <returns>An <see cref="All"/> parameters set.</returns>
        public static new All FromDictionary(Dictionary<string, object>? dictionary)
        {
            if (dictionary == null) return new All();
            return new All
            {
                PageSize = dictionary.GetOrNullInt("page_size"),
                BeforeId = dictionary.GetOrNull<string>("before_id"),
                AfterId = dictionary.GetOrNull<string>("after_id"),
            };
        }
    }
}
