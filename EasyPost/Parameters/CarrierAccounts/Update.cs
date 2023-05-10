using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.CarrierAccounts
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#update-a-carrieraccount">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Update(string, Update, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Update : BaseParameters
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
    }
}
