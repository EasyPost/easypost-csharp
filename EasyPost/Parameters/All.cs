using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public sealed class All : RequestParameters
    {
        [ApiCompatibility(ApiVersion.Latest)]
        [RequestParameter(Necessity.Optional, "after_id")]
        public string? AfterId { get; set; }
        [ApiCompatibility(ApiVersion.Latest)]
        [RequestParameter(Necessity.Optional, "before_id")]
        public string? BeforeId { get; set; }
        [ApiCompatibility(ApiVersion.Latest)]
        [RequestParameter(Necessity.Optional, "end_datetime")]
        public string? EndDatetime { get; set; }
        [ApiCompatibility(ApiVersion.Latest)]
        [RequestParameter(Necessity.Optional, "page_size")]
        public int? PageSize { get; set; }
        [ApiCompatibility(ApiVersion.Latest)]
        [RequestParameter(Necessity.Optional, "start_datetime")]
        public string? StartDatetime { get; set; }

        public All(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
        {
        }

        internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
        {
            return ToDictionary(this, client);
        }
    }
}
