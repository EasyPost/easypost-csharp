using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters.Beta
{
    public static class EndShippers
    {
        public sealed class Update : RequestParameters
        {
            #region Request Parameters

            // Must send all parameters when updating an EndShipper

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Required, "address", "street1")]
            public string? Street1 { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Required, "address", "street2")]
            public string? Street2 { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Required, "address", "city")]
            public string? City { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Required, "address", "state")]
            public string? State { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Required, "address", "zip")]
            public string? Zip { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Required, "address", "country")]
            public string? Country { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Required, "address", "name")]
            public string? Name { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Required, "address", "company")]
            public string? Company { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Required, "address", "phone")]
            public string? Phone { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Required, "address", "email")]
            public string? Email { internal get; set; }

            #endregion

            public Update(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }
        }

        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Optional, "address", "street1")]
            public string? Street1 { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Optional, "address", "street2")]
            public string? Street2 { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Optional, "address", "city")]
            public string? City { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Optional, "address", "state")]
            public string? State { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Optional, "address", "zip")]
            public string? Zip { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Optional, "address", "country")]
            public string? Country { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Optional, "address", "name")]
            public string? Name { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Optional, "address", "company")]
            public string? Company { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Optional, "address", "phone")]
            public string? Phone { internal get; set; }

            [ApiCompatibility(ApiVersion.Beta)]
            [RequestParameter(Necessity.Optional, "address", "email")]
            public string? Email { internal get; set; }

            #endregion

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }
        }
    }
}
