using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Shipments
{
    // TODO: Fix this when the overload is introduced.
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.ShipmentService.All(All)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class All : BaseParameters
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
    }
}
