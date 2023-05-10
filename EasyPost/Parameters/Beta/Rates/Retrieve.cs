using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Beta.Rates
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#retrieve-rates-for-a-shipment">Parameters</a> for <see cref="EasyPost.Services.Beta.RateService.RetrieveStatelessRates(Retrieve, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Retrieve : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     List of <see cref="Models.API.Address"/>es (or <see cref="Parameters.Addresses.Create"/> parameters) to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        /// <summary>
        ///     List of <see cref="Models.API.Address"/>es (or <see cref="Parameters.Addresses.Create"/> parameters) to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        /// <summary>
        ///     Reference name to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     <see cref="Models.API.Parcel"/> (or <see cref="Parameters.Parcels.Create"/> parameters) to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "parcel")]
        public IParcelParameter? Parcel { get; set; }

        /// <summary>
        ///     List of <see cref="Models.API.CarrierAccount"/>s to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        ///     The provided <see cref="Models.API.CarrierAccount"/>s must exist prior to making the API call.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; set; } // carrier accounts have to exist first, can't be made during this call

        /// <summary>
        ///     Service name to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "service")]
        public string? Service { get; set; }

        #endregion
    }
}
