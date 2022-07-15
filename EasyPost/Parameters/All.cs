using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public sealed class All : RequestParameters
    {
        #region Request Parameters

        [ApiCompatibility(ApiVersion.Latest, ApiVersion.Beta)]
        [RequestParameter(Necessity.Optional, "after_id")]
        public string? AfterId { internal get; set; }
        [ApiCompatibility(ApiVersion.Latest, ApiVersion.Beta)]
        [RequestParameter(Necessity.Optional, "before_id")]
        public string? BeforeId { internal get; set; }
        [ApiCompatibility(ApiVersion.Latest, ApiVersion.Beta)]
        [RequestParameter(Necessity.Optional, "end_datetime")]
        public string? EndDatetime { internal get; set; }
        [ApiCompatibility(ApiVersion.Latest, ApiVersion.Beta)]
        [RequestParameter(Necessity.Optional, "page_size")]
        public int? PageSize { internal get; set; }
        [ApiCompatibility(ApiVersion.Latest, ApiVersion.Beta)]
        [RequestParameter(Necessity.Optional, "start_datetime")]
        public string? StartDatetime { internal get; set; }

        #endregion

        public All(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
        {
        }

        internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
        {
            return ToDictionary(this, client);
        }
    }
}
