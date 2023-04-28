using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.BetaFeatures.Parameters.ReferralCustomers
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.PartnerService.All(All)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class All : BaseAllParameters
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
        ///     Only return records created before this timestamp. Defaults to 1 month ago, or 1 month before a passed <see cref="StartDatetime"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "end_datetime")]
        public string? EndDatetime { get; set; }

        /// <summary>
        ///     The number of records to return on each page. The maximum value is 100, and default is 20.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "page_size")]
        public int? PageSize { get; set; } = 20;

        /// <summary>
        ///     Only return records created after this timestamp. Defaults to 1 month ago, or 1 month before a passed <see cref="EndDatetime"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "start_datetime")]
        public string? StartDatetime { get; set; }

        #endregion

        protected override TParameters FromDictionaryProtected<TParameters>(Dictionary<string, object> dictionary)
        {
            var parameters = new Addresses.All
            {
                PageSize = dictionary.GetOrNullInt("page_size"),
                BeforeId = dictionary.GetOrNull<string>("before_id"),
                AfterId = dictionary.GetOrNull<string>("after_id"),
                StartDatetime = dictionary.GetOrNull<string>("start_datetime"),
                EndDatetime = dictionary.GetOrNull<string>("end_datetime"),
            };

            return (parameters as TParameters)!;
        }
    }
}
