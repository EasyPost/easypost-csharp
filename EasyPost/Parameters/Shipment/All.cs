using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-shipments">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.All(All, System.Threading.CancellationToken)"/> API calls.
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
        public int? PageSize { get; set; }

        /// <summary>
        ///     Only return records created after this timestamp. Defaults to 1 month ago, or 1 month before a passed <see cref="EndDatetime"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "start_datetime")]
        public string? StartDatetime { get; set; }

        /// <summary>
        ///     If true, only return purchased shipments. Defaults to true.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "purchased")]
        public bool? Purchased { get; set; }

        /// <summary>
        ///     If true, also include shipments created by child users.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "include_children")]
        public bool? IncludeChildren { get; set; }

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
                StartDatetime = dictionary.GetOrNull<string>("start_datetime"),
                EndDatetime = dictionary.GetOrNull<string>("end_datetime"),
                Purchased = dictionary.GetOrNullBoolean("purchased"),
                IncludeChildren = dictionary.GetOrNullBoolean("include_children"),
            };
        }
    }
}
