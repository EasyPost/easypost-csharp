using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.CarrierAccount
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/carrier-accounts#update-a-carrieraccount">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Update(string, AUpdate, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Update : AUpdate
    {
        #region Request Parameters

        /// <summary>
        ///     Credentials to update for the <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "credentials")]
        public Dictionary<string, object?>? Credentials { get; set; }

        /// <summary>
        ///     Description to update for the <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "description")]
        public string? Description { get; set; }

        /// <summary>
        ///     Reference name to update for the <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     Test credentials to update for the <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "test_credentials")]
        // ReSharper disable once InconsistentNaming
        public Dictionary<string, object?>? TestCredentials { get; set; }

        #endregion

        /// <inheritdoc />
        internal override bool ValidCarrierAccountType(string type) => !Constants.CarrierAccounts.IsCustomWorkflowUpdate(type);

        /// <inheritdoc />
        internal override string Endpoint(string id) => $"carrier_accounts/{id}";
    }

    /// <summary>
    ///     The base class for all carrier-account update parameter sets.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class AUpdate : BaseParameters<Models.API.CarrierAccount>
    {
        #region Request Parameters

        /// <summary>
        ///     Check if the provided carrier account type is valid for this parameter set.
        /// </summary>
        /// <param name="type">The carrier account type to check.</param>
        /// <returns>True if the carrier account type is valid; otherwise, false.</returns>
        internal abstract bool ValidCarrierAccountType(string type);

        /// <summary>
        ///     Get the endpoint for the carrier account update.
        /// </summary>
        /// <param name="id">The ID of the carrier account to update.</param>
        /// <returns>The endpoint for the carrier account update.</returns>
        internal abstract string Endpoint(string id);

        #endregion
    }
}
