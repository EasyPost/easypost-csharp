using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters.CarrierAccount
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/carrier-accounts#create-a-carrieraccount">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Create(ACreate, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateOauth : ACreate
    {
        #region Request Parameters

        /// <summary>
        ///     Description for the new <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account_oauth_registrations", "description")]
        public string? Description { get; set; }

        /// <summary>
        ///     Reference name for the new <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account_oauth_registrations", "reference")]
        public string? Reference { get; set; }

        /// <inheritdoc/>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account_oauth_registrations", "type")]
        public override string? Type { get; set; }

        #endregion

        /// <inheritdoc cref="EasyPost.Parameters.CarrierAccount.ACreate.Endpoint"/>
        internal override string Endpoint => Constants.CarrierAccounts.OauthCreateEndpoint;

        /// <inheritdoc />
        public override Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dictionary = base.ToDictionary();

            dictionary!.AddOrUpdate(Type, "type"); // Need to add "type" top-level (added sub-level by serialization)

            return dictionary;
        }
    }
}
