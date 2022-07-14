using System.Collections.Generic;
using EasyPost._base;

namespace EasyPost.Parameters
{
    public static class Reports
    {
        public sealed class Create : ApiParameters
        {
            [Parameter(Necessity.Required, "end_date")]
            public string? EndDate { internal get; set; }
            [Parameter(Necessity.Required, "start_date")]
            public string? StartDate { internal get; set; }

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                return ToDictionary(this);
            }
        }
    }
}
