using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.Beta.Rate
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#retrieve-rates-for-a-shipment">Parameters</a> for <see cref="EasyPost.Services.Beta.RateService.RetrieveStatelessRates(Retrieve, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Retrieve : BaseParameters<Models.API.Beta.StatelessRate>
    {
        #region Request Parameters

        /// <summary>
        ///     List of <see cref="Models.API.Address"/>es (or <see cref="Parameters.Address.Create"/> parameters) to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        /// <summary>
        ///     List of <see cref="Models.API.Address"/>es (or <see cref="Parameters.Address.Create"/> parameters) to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        /// <summary>
        ///     Additional <see cref="Models.API.Options"/> to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "options")]
        public Models.API.Options? Options { get; set; }

        /// <summary>
        ///     Reference name to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     <see cref="Models.API.Parcel"/> (or <see cref="Parameters.Parcel.Create"/> parameters) to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "parcel")]
        public IParcelParameter? Parcel { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "shipment", "carrier_accounts")]
        // Internal, this is not accessible to the end-user
        internal List<string>? CarrierAccountIds { get; set; }

        /// <summary>
        ///     List of <see cref="Models.API.CarrierAccount"/>s to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        ///     The provided <see cref="Models.API.CarrierAccount"/>s must exist prior to making the API call.
        /// </summary>
        // This is an alias for CarrierAccountIds, the real parameter sent to the API. This is not included in the final payload.
        // ReSharper disable once CollectionNeverUpdated.Global
        public List<Models.API.CarrierAccount>? CarrierAccounts { get; set; } // carrier accounts have to exist first, can't be made during this call

        /// <summary>
        ///     Service name to use to retrieve <see cref="Models.API.Beta.StatelessRate"/>s.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "shipment", "service")]
        public string? Service { get; set; }

        #endregion

        /// <summary>
        ///     Override the default <see cref="BaseParameters{TMatchInputType}.ToDictionary"/> method to handle the unique serialization requirements for this parameter set.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/>.</returns>
        public override Dictionary<string, object> ToDictionary()
        {
            // Copy the IDs of any CarrierAccounts into the CarrierAccountIds list
            CarrierAccountIds = CarrierAccounts?.Select(x => x.Id!).ToList();

            return base.ToDictionary();
        }
    }
}
