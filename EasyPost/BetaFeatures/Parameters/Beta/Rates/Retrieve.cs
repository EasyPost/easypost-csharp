using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Beta.Rates
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.Beta.RateService.RetrieveStatelessRates(Retrieve)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Retrieve : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "reference")]
        public string? Reference { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "parcel")]
        public IParcelParameter? Parcel { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
        public List<CarrierAccount>? CarrierAccounts { get; set; } // carrier accounts have to exist first, can't be made during this call

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "service")]
        public string? Service { get; set; }

        #endregion
    }
}
