using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters
{
    public class All : EasyPostParameters
    {
        [Parameter("after_id")]
        public string? AfterId { get; set; }
        [Parameter("before_id")]
        public string? BeforeId { get; set; }

        [Parameter("end_datetime")]
        public string? EndDatetime { get; set; }

        [Parameter("page_size")]
        public int? PageSize { get; set; }

        [Parameter("start_datetime")]
        public string? StartDatetime { get; set; }

        public All(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
        {
        }

        internal override Dictionary<string, object?>? ToDictionary()
        {
            RegisterParameters(this);
            return ParameterDictionary;
        }
    }
}
