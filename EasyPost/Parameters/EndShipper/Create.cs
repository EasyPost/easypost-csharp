using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.EndShipper
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/endshippers#create-an-endshipper">Parameters</a> for <see cref="EasyPost.Services.EndShipperService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Create : BaseParameters<Models.API.EndShipper>, IEndShipperParameter
    {
        #region Request Parameters

        /// <summary>
        ///     City for the new <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "city")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "city")]
        public string? City { get; set; }

        /// <summary>
        ///     Company for the new <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "company")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "company")]
        public string? Company { get; set; }

        /// <summary>
        ///     Country for the new <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "country")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "country")]
        public string? Country { get; set; }

        /// <summary>
        ///     Email address for the new <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "email")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "email")]
        public string? Email { get; set; }

        /// <summary>
        ///     Name for the new <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "name")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Phone number for the new <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "phone")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "phone")]
        public string? Phone { get; set; }

        /// <summary>
        ///     State for the new <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "state")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "state")]
        public string? State { get; set; }

        /// <summary>
        ///     First street line for the new <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "street1")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "street1")]
        public string? Street1 { get; set; }

        /// <summary>
        ///     Second street line for the new <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "street2")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "street2")]
        public string? Street2 { get; set; }

        /// <summary>
        ///     ZIP code for the new <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "zip")]
        [NestedRequestParameter(typeof(Shipment.Create), Necessity.Optional, "zip")]
        public string? Zip { get; set; }

        #endregion
    }
}
