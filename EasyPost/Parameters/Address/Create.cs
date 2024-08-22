using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Address
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/addresses#verify-an-address">Parameters</a> for <see cref="EasyPost.Services.AddressService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Create : BaseParameters<Models.API.Address>, IAddressParameter
    {
        #region Request Parameters

        /// <summary>
        ///     ID of the <see cref="Models.API.Address"/> to reference in this request.
        ///     ID is not used when calling <see cref="Services.AddressService.Create(Create, System.Threading.CancellationToken)"/>,
        ///     but is used when using this parameter set as a nested parameter set in other API calls.
        /// </summary>
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Carrier facility for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "carrier_facility")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "carrier_facility")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "carrier_facility")]
        public string? CarrierFacility { get; set; }

        /// <summary>
        ///     City for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "city")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "city")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "city")]
        public string? City { get; set; }

        /// <summary>
        ///     Company name for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "company")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "company")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "company")]
        public string? Company { get; set; }

        /// <summary>
        ///     Country for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "country")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "country")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "country")]
        public string? Country { get; set; }

        /// <summary>
        ///     Email address for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "email")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "email")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "email")]
        public string? Email { get; set; }

        /// <summary>
        ///     Federal tax ID for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "federal_tax_id")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "federal_tax_id")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "federal_tax_id")]
        public string? FederalTaxId { get; set; }

        /// <summary>
        ///     Name for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "name")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "name")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Phone number for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "phone")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "phone")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "phone")]
        public string? Phone { get; set; }

        /// <summary>
        ///     Whether the <see cref="Models.API.Address"/> is residential.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "residential")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "residential")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "residential")]
        public bool? Residential { get; set; }

        /// <summary>
        ///     State for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "state")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "state")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "state")]
        public string? State { get; set; }

        /// <summary>
        ///     State tax ID for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "state_tax_id")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "state_tax_id")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "state_tax_id")]
        public string? StateTaxId { get; set; }

        /// <summary>
        ///     First street line for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "street1")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "street1")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "street1")]
        public string? Street1 { get; set; }

        /// <summary>
        ///     Second street line for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "street2")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "street2")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "street2")]
        public string? Street2 { get; set; }

        /// <summary>
        ///     Whether to enforce strict verification for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "verify_strict")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "verify_strict")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "verify_strict")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "verify_strict")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "verify_strict")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "verify_strict")]
        public bool? VerifyStrict { get; set; }

        /// <summary>
        ///     Whether to enforce verification for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "verify")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "verify")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "verify")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "verify")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "verify")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "verify")]
        public bool? Verify { get; set; }

        /// <summary>
        ///     ZIP code for the new <see cref="Models.API.Address"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "zip")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Order.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Pickup.Create), Necessity.Optional, "zip")]
        [NestedRequestParameter(typeof(Beta.Rate.Retrieve), Necessity.Optional, "zip")]
        public string? Zip { get; set; }

        #endregion
    }
}
