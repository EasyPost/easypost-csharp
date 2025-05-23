using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters.CarrierAccount
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/carrier-accounts#create-a-carrieraccount">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Create(ACreate, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateUpsMailInnovations : ACreate
    {
        #region Request Parameters

        /// <summary>
        ///     The UPS account number.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account_oauth_registrations", "account_number")]
        public string? AccountNumber { get; set; }

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

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateUpsMailInnovations"/> class.
        /// </summary>
        public CreateUpsMailInnovations()
            : base(CarrierAccountType.UpsMailInnovations)
        {
        }

        /// <inheritdoc />
        public override Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dictionary = base.ToDictionary();

            dictionary!.AddOrUpdate(Type, "type"); // Need to add "type" top-level (added sub-level by serialization)

            return dictionary;
        }
    }
}
