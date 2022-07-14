using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public static class Addresses
    {
        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Required, "address")]
            public Models.API.Address? Address { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "verify_strict")]
            public List<string>? ToStrictVerify { internal get; set; } = null;

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "verify")]
            public List<string>? ToVerify { internal get; set; } = null;

            #endregion

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }

        public sealed class Verify : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "verify_strict")]
            public List<string>? ToStrictVerify { internal get; set; } = null;

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "verify")]
            public List<string>? ToVerify { internal get; set; } = null;

            #endregion

            public Verify(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }

            internal override Dictionary<string, object?>? ToDictionary(EasyPostClient client)
            {
                return ToDictionary(this, client);
            }
        }
    }
}
