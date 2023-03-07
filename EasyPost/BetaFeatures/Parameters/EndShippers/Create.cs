using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.EndShippers
{
    public class Create : BaseParameters, IEndShipperParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "address", "carrier_facility")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "carrier_facility")]
        public string? CarrierFacility { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "city")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "city")]
        public string? City { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "company")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "company")]
        public string? Company { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "country")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "country")]
        public string? Country { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "email")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "email")]
        public string? Email { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "federal_tax_id")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "federal_tax_id")]
        public string? FederalTaxId { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "name")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "name")]
        public string? Name { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "phone")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "phone")]
        public string? Phone { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "residential")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "residential")]
        public bool? Residential { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "state")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "state")]
        public string? State { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "state_tax_id")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "state_tax_id")]
        public string? StateTaxId { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "street1")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "street1")]
        public string? Street1 { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "street2")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "street2")]
        public string? Street2 { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "verify_strict")]
        // "verify_strict" is not included when address creation parameters are used for a to/from address in a shipment creation request.
        public bool? VerifyStrict { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "verify")]
        // "verify" is not included when address creation parameters are used for a to/from address in a shipment creation request.
        public bool? Verify { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "address", "zip")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "zip")]
        public string? Zip { get; set; }

        #endregion
    }
}
