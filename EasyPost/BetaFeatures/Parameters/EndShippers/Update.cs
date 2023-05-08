using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.EndShippers
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#update-an-endshipper">Parameters</a> for <see cref="EasyPost.Services.EndShipperService.Update(string, Update, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Update : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     City to update for the <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "city")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "city")]
        public string? City { get; set; }

        /// <summary>
        ///     Company to update for the <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "company")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "company")]
        public string? Company { get; set; }

        /// <summary>
        ///     Country to update for the <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "country")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "country")]
        public string? Country { get; set; }

        /// <summary>
        ///     Email address to update for the <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "email")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "email")]
        public string? Email { get; set; }

        /// <summary>
        ///     Name to update for the <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "name")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Phone number to update for the <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "phone")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "phone")]
        public string? Phone { get; set; }

        /// <summary>
        ///     State to update for the <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "state")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "state")]
        public string? State { get; set; }

        /// <summary>
        ///     First street line to update for the <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "street1")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "street1")]
        public string? Street1 { get; set; }

        /// <summary>
        ///     Second street line to update for the <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "street2")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "street2")]
        public string? Street2 { get; set; }

        /// <summary>
        ///     ZIP code to update for the <see cref="Models.API.EndShipper"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address", "zip")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "zip")]
        public string? Zip { get; set; }

        #endregion
    }
}
