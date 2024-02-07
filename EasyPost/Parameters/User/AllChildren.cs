using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters.User
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.UserService.AllChildren(AllChildren, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AllChildren : BaseAllParameters<Models.API.User>
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
        [Obsolete("This parameter is not supported by the API.")]
        [TopLevelRequestParameter(Necessity.Optional, "before_id")]
        public string? BeforeId { get; set; }

        /// <summary>
        ///     Only return records created before this timestamp. Defaults to 1 month ago, or 1 month before a passed <see cref="StartDatetime"/>.
        /// </summary>
        [Obsolete("This parameter is not supported by the API.")]
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
        [Obsolete("This parameter is not supported by the API.")]
        [TopLevelRequestParameter(Necessity.Optional, "start_datetime")]
        public string? StartDatetime { get; set; }

        #endregion

        /// <summary>
        ///     Convert a dictionary into this parameter set.
        /// </summary>
        /// <param name="dictionary">Dictionary to parse.</param>
        /// <returns>An <see cref="AllChildren"/> parameters set.</returns>
        public static new AllChildren FromDictionary(Dictionary<string, object>? dictionary)
        {
            if (dictionary == null) return new AllChildren();
            return new AllChildren
            {
                PageSize = dictionary.GetOrNullInt("page_size"),
                AfterId = dictionary.GetOrNull<string>("after_id"),
            };
        }
    }
}
