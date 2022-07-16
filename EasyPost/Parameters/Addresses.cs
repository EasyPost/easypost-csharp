using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;

namespace EasyPost.Parameters
{
    public static class Addresses
    {
        /*
         * NOTES:
         * Per https://www.informit.com/articles/article.aspx?p=1997935&seqNum=5 and https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values,
         * Any nullable object (non-primitive) will default to `null`
         * Any nullable primitive will default to `null`
         * No need to set a default value for Optional parameters, will be `null` if not set, which is what the internal validator expects
         */

        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "street1")]
            public string? Street1 { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "street2")]
            public string? Street2 { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "city")]
            public string? City { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "state")]
            public string? State { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "zip")]
            public string? Zip { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "country")]
            public string? Country { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "name")]
            public string? Name { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "company")]
            public string? Company { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "phone")]
            public string? Phone { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "email")]
            public string? Email { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "residential")]
            public bool? Residential { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "carrier_facility")]
            public string? CarrierFacility { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "federal_tax_id")]
            public string? FederalTaxId { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "address", "state_tax_id")]
            public string? StateTaxId { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "verify")]
            public bool? ToVerify { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "verify_strict")]
            public bool? ToStrictVerify { internal get; set; }

            #endregion

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }
        }
    }
}
