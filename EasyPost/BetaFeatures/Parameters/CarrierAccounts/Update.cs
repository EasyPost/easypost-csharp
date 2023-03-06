using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.CarrierAccounts
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.CarrierAccount.Update"/> API calls.
    /// </summary>
    public class Update : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "credentials")]
        public Dictionary<string, object?>? Credentials { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "description")]
        public string? Description { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "reference")]
        public string? Reference { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "test_credentials")]
        // ReSharper disable once InconsistentNaming
        public Dictionary<string, object?>? TestCredentials { get; set; }

        #endregion
    }
}
