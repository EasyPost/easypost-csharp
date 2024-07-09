using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.CarrierAccount
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#update-a-carrieraccount">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Update(string, AUpdate, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UpdateUpsSurePost : AUpdate
    {
        #region Request Parameters

        /// <summary>
        ///     Credentials to update for the <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "ups_oauth_registrations", "account_number")]
        public string? AccountNumber { get; set; }

        /// <summary>
        ///     Description to update for the <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "ups_oauth_registrations", "description")]
        public string? Description { get; set; }

        /// <summary>
        ///     Reference name to update for the <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "ups_oauth_registrations", "reference")]
        public string? Reference { get; set; }

        #endregion

        /// <inheritdoc />
        internal override bool ValidCarrierAccountType(string type) => Constants.CarrierAccounts.IsCustomWorkflowUpdate(type);

        /// <inheritdoc />
        internal override string Endpoint(string id) => $"ups_oauth_registrations/{id}";
    }
}
