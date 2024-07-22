using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters.Claim
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-insurances">Parameters</a> for <see cref="EasyPost.Services.ClaimService.All(All, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class All : BaseAllParameters<Models.API.Claim>
    {
        #region Request Parameters

        /// <summary>
        ///     Only return records of a particular <see cref="Models.API.ClaimType"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "type")]
        public Models.API.ClaimType? Type { get; set; }

        /// <summary>
        ///     Only return records of a particular status.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "status")]
        public string? Status { get; set; }

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
        public int? PageSize { get; set; }

        /// <summary>
        ///     Only return records created after this timestamp. Defaults to 1 month ago, or 1 month before a passed <see cref="EndDatetime"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "start_datetime")]
        public string? StartDatetime { get; set; }

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
                Type = dictionary.GetOrNullEnum<Models.API.ClaimType>("type"),
                Status = dictionary.GetOrNull<string>("status"),
                PageSize = dictionary.GetOrNullInt("page_size"),
                BeforeId = dictionary.GetOrNull<string>("before_id"),
                AfterId = dictionary.GetOrNull<string>("after_id"),
                StartDatetime = dictionary.GetOrNull<string>("start_datetime"),
                EndDatetime = dictionary.GetOrNull<string>("end_datetime"),
            };
        }
    }
}
