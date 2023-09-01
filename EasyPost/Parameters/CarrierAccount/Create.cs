using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.CarrierAccount
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-carrier-account">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Create : ACreate
    {
        #region Request Parameters

        /// <summary>
        ///     Credentials for the new <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "credentials")]
        public Dictionary<string, object?>? Credentials { get; set; }

        /// <summary>
        ///     Test credentials for the new <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "test_credentials")]
        // ReSharper disable once InconsistentNaming
        public Dictionary<string, object?>? TestCredentials { get; set; }

        /// <summary>
        ///     The endpoint to hit to create this carrier account.
        /// </summary>
        internal override string Endpoint => Constants.CarrierAccounts.StandardCreateEndpoint;

        #endregion

        /// <summary>
        ///     Create a new <see cref="Create"/> instance.
        /// </summary>
        [Obsolete("Use Create(CarrierAccountType type) instead.")]
        public Create() : base()
        {
        }

        /// <summary>
        ///     Create a new <see cref="Create"/> instance.
        /// </summary>
        /// <param name="type">The type of carrier account to create.</param>
        public Create(CarrierAccountType type) : base(type)
        {
        }
    }

    /// <summary>
    ///     The base class for all carrier-account creation parameter sets.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class ACreate : BaseParameters<Models.API.CarrierAccount>, ICarrierAccountParameter
    {
        #region Request Parameters

        /// <summary>
        ///     Description for the new <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "description")]
        public string? Description { get; set; }

        /// <summary>
        ///     Reference name for the new <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     Type of <see cref="Models.API.CarrierAccount"/> to create.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "type")]
        public string? Type { get; set; }

        /// <summary>
        ///     The endpoint to hit to create this carrier account.
        /// </summary>
        internal abstract string Endpoint { get; }

        /// <summary>
        ///     Create a new <see cref="ACreate"/> instance.
        /// </summary>
        protected ACreate()
        {
        }

        /// <summary>
        ///     Create a new <see cref="ACreate"/> instance.
        /// </summary>
        /// <param name="type">The type of carrier account to create.</param>
        protected ACreate(CarrierAccountType type)
        {
            Type = type.Name;
        }

        #endregion
    }
}
