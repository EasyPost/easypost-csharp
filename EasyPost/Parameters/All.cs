using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public class All : ApiParameters
    {
        [Parameter(Necessity.Optional, "after_id")]
        public string? AfterId { get; set; }
        [Parameter(Necessity.Optional, "before_id")]
        public string? BeforeId { get; set; }

        [Parameter(Necessity.Optional, "end_datetime")]
        public string? EndDatetime { get; set; }

        [Parameter(Necessity.Optional, "page_size")]
        public int? PageSize { get; set; }

        [Parameter(Necessity.Optional, "start_datetime")]
        public string? StartDatetime { get; set; }

        public All(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
        {
        }

        internal override Dictionary<string, object?>? ToDictionary()
        {
            return ToDictionary(this);
        }
    }
}
