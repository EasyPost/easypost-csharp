using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters
{
    public static class Addresses
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.AddressService.Create"/> API calls.
        /// </summary>
        public class Create : Parameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "address", "carrier_facility")]
            public string? CarrierFacility { get; set; }

            [RequestParameter(Necessity.Optional, "address", "city")]
            public string? City { get; set; }

            [RequestParameter(Necessity.Optional, "address", "company")]
            public string? Company { get; set; }

            [RequestParameter(Necessity.Optional, "address", "country")]
            public string? Country { get; set; }

            [RequestParameter(Necessity.Optional, "address", "email")]
            public string? Email { get; set; }

            [RequestParameter(Necessity.Optional, "address", "federal_tax_id")]
            public string? FederalTaxId { get; set; }

            [RequestParameter(Necessity.Optional, "address", "name")]
            public string? Name { get; set; }

            [RequestParameter(Necessity.Optional, "address", "phone")]
            public string? Phone { get; set; }

            [RequestParameter(Necessity.Optional, "address", "residential")]
            public bool Residential { get; set; } = false;

            [RequestParameter(Necessity.Optional, "address", "state")]
            public string? State { get; set; }

            [RequestParameter(Necessity.Optional, "address", "state_tax_id")]
            public string? StateTaxId { get; set; }

            [RequestParameter(Necessity.Optional, "address", "street1")]
            public string? Street1 { get; set; }

            [RequestParameter(Necessity.Optional, "address", "street2")]
            public string? Street2 { get; set; }

            [RequestParameter(Necessity.Optional, "verify_strict")]
            public bool VerifyStrictly { get; set; } = false;

            [RequestParameter(Necessity.Optional, "verify")]
            public bool Verify { get; set; } = false;

            [RequestParameter(Necessity.Optional, "address", "zip")]
            public string? Zip { get; set; }

            #endregion
        }
    }
}
