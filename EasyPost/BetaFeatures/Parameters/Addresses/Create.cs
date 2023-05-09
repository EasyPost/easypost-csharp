using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Addresses
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-and-verify-addresses">Parameters</a> for <see cref="EasyPost.Services.AddressService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IAddressParameter
    {
        #region Request Parameters

        /// <summary>
        ///     ID of the <see cref="Models.API.Address"/> to reference in this request.
        ///     ID is not used when calling <see cref="Services.AddressService.Create(Create, System.Threading.CancellationToken)"/>,
        ///     but is used when using this parameter set as a nested parameter set in other API calls.
        /// </summary>
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Carrier facility for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "carrier_facility")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "carrier_facility")]
        public string? CarrierFacility { get; set; }

        /// <summary>
        ///     City for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "city")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "city")]
        public string? City { get; set; }

        /// <summary>
        ///     Company name for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "company")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "company")]
        public string? Company { get; set; }

        /// <summary>
        ///     Country for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "country")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "country")]
        public string? Country { get; set; }

        /// <summary>
        ///     Email address for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "email")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "email")]
        public string? Email { get; set; }

        /// <summary>
        ///     Federal tax ID for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "federal_tax_id")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "federal_tax_id")]
        public string? FederalTaxId { get; set; }

        /// <summary>
        ///     Name for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "name")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Phone number for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "phone")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "phone")]
        public string? Phone { get; set; }

        /// <summary>
        ///     Whether the <see cref="Models.API.Address"/> is residential.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "residential")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "residential")]
        public bool? Residential { get; set; }

        /// <summary>
        ///     State for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "state")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "state")]
        public string? State { get; set; }

        /// <summary>
        ///     State tax ID for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "state_tax_id")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "state_tax_id")]
        public string? StateTaxId { get; set; }

        /// <summary>
        ///     First street line for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "street1")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "street1")]
        public string? Street1 { get; set; }

        /// <summary>
        ///     Second street line for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "street2")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "street2")]
        public string? Street2 { get; set; }

        /// <summary>
        ///     Whether to enforce strict verification for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "verify_strict")]
        // "verify_strict" is not included when address creation parameters are used in a non-address creation request.
        public bool? VerifyStrict { get; set; }

        /// <summary>
        ///     Whether to enforce verification for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "verify")]
        // "verify" is not included when address creation parameters are used in a non-address creation request.
        public bool? Verify { get; set; }

        /// <summary>
        ///     ZIP code for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "zip")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "zip")]
        public string? Zip { get; set; }

        #endregion
    }
}
