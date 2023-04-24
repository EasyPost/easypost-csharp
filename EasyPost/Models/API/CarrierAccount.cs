using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CarrierAccount : EasyPostObject, ICarrierAccountParameter
    {
        #region JSON Properties

        [JsonProperty("billing_type")]
        public string? BillingType { get; set; }
        [JsonProperty("credentials")]
        public Dictionary<string, object>? Credentials { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("readable")]
        public string? Readable { get; set; }
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("test_credentials")]
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "TestCredentials is the correct name for this property")]
        public Dictionary<string, object>? TestCredentials { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion

        internal CarrierAccount()
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Update this CarrierAccount.
        /// </summary>
        /// <param name="parameters">See CarrierAccount.Create for more details.</param>
        /// <returns>The updated CarrierAccount.</returns>
        [CrudOperations.Update]
        public async Task<CarrierAccount> Update(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("carrier_account");
            await Update<CarrierAccount>(Http.Method.Patch, $"carrier_accounts/{Id}", parameters);
            return this;
        }

        /// <summary>
        ///     Update this <see cref="CarrierAccount"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.CarrierAccounts.Update"/> parameter set.</param>
        /// <returns>This updated <see cref="CarrierAccount"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<CarrierAccount> Update(BetaFeatures.Parameters.CarrierAccounts.Update parameters)
        {
            await Update<CarrierAccount>(Http.Method.Patch, $"carrier_accounts/{Id}", parameters.ToDictionary());
            return this;
        }

        /// <summary>
        ///     Remove this CarrierAccount from your account.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        [CrudOperations.Delete]
        public async Task Delete() => await DeleteNoResponse($"carrier_accounts/{Id}");

        #endregion
    }
}
