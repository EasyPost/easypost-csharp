using System.Collections.Generic;
using EasyPost.Interfaces;

namespace EasyPost.Parameters.V2
{
    public static class Reports
    {
        public class Create : EasyPostParameters
        {
            [Parameter("end_date")]
            public string? EndDate { internal get; set; }
            [Parameter("start_date")]
            public string? StartDate { internal get; set; }

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary()
            {
                RegisterParameters(this);
                return ParameterDictionary;
            }
        }
    }
}
