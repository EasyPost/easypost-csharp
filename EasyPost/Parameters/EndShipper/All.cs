using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters.EndShipper
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/endshippers#retrieve-all-endshippers">Parameters</a> for <see cref="EasyPost.Services.EndShipperService.All(All, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class All : BaseAllParameters<Models.API.EndShipper>
    {
        #region Request Parameters

        /// <summary>
        ///     Only records created after the given ID will be included.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "after_id")]
        public string? AfterId { get; set; }

        /// <summary>
        ///     The number of records to return on each page. The maximum value is 100, and default is 20.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "page_size")]
        public int? PageSize { get; set; }

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
                AfterId = dictionary.GetOrNull<string>("after_id"),
            };
        }
    }
}
