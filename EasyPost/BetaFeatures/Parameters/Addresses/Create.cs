using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Addresses
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.AddressService.Create(Create)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IAddressParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "address", "carrier_facility")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "carrier_facility")]
        public string? CarrierFacility { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "city")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "city")]
        public string? City { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "company")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "company")]
        public string? Company { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "country")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "country")]
        public string? Country { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "email")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "email")]
        public string? Email { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "federal_tax_id")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "federal_tax_id")]
        public string? FederalTaxId { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "name")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "name")]
        public string? Name { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "phone")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "phone")]
        public string? Phone { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "residential")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "residential")]
        public bool? Residential { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "state")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "state")]
        public string? State { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "state_tax_id")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "state_tax_id")]
        public string? StateTaxId { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "street1")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "street1")]
        public string? Street1 { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "street2")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "street2")]
        public string? Street2 { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "verify_strict")]
        // "verify_strict" is not included when address creation parameters are used in a non-address creation request.
        public bool? VerifyStrict { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "verify")]
        // "verify" is not included when address creation parameters are used in a non-address creation request.
        public bool? Verify { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "zip")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "zip")]
        public string? Zip { get; set; }

        #endregion
    }
}
