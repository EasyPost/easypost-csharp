using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public static class Reports
    {
        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            // TODO: What are the other report options?

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "report", "end_date")]
            public string? EndDate { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "report", "start_date")]
            public string? StartDate { internal get; set; }

            #endregion

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
