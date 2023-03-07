using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.EndShippers
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.EndShipper.Update"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Update : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "address", "carrier_facility")]
        public string? CarrierFacility { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "city")]
        public string? City { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "company")]
        public string? Company { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "country")]
        public string? Country { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "email")]
        public string? Email { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "federal_tax_id")]
        public string? FederalTaxId { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "name")]
        public string? Name { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "phone")]
        public string? Phone { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "residential")]
        public bool? Residential { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "state")]
        public string? State { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "state_tax_id")]
        public string? StateTaxId { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "street1")]
        public string? Street1 { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "street2")]
        public string? Street2 { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "zip")]
        public string? Zip { get; set; }

        #endregion
    }
}
