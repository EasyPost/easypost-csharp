using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CarrierAccountService : EasyPostService
    {
        internal CarrierAccountService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations
        
        // TODO: Use ID or whole object as parameter?

        /// <summary>
        ///     Create a CarrierAccount.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"type", string} Required (e.g. EndiciaAccount, UPSAccount, etc.).
        ///     * {"reference", string} External reference for carrier account.
        ///     * {"description", string} Description of carrier account.
        ///     * {"credentials", Dictionary&lt;string, string&gt;}
        ///     * {"test_credentials", Dictionary&lt;string, string&gt;}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        [CrudOperations.Create]
        public async Task<CarrierAccount> Create(Dictionary<string, object> parameters)
        {
            if (parameters["type"] is not string carrierType)
            {
                throw new MissingParameterError("CarrierAccount type is required.");
            }

            string endpoint = SelectCarrierAccountCreationEndpoint(carrierType);

            parameters = parameters.Wrap("carrier_account");

            return await Create<CarrierAccount>(endpoint, parameters);
        }

        /// <summary>
        ///     Create a <see cref="CarrierAccount"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.CarrierAccounts.Create"/> parameter set.</param>
        /// <returns><see cref="CarrierAccount"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<CarrierAccount> Create(BetaFeatures.Parameters.CarrierAccounts.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            if (parameters.Type == null)
            {
                throw new MissingParameterError(nameof(parameters.Type));
            }

            string endpoint = SelectCarrierAccountCreationEndpoint(parameters.Type);

            return await Create<CarrierAccount>(endpoint, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all available carrier accounts.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierAccount instances.</returns>
        [CrudOperations.Read]
        public async Task<List<CarrierAccount>> All() => await List<List<CarrierAccount>>("carrier_accounts");

        /// <summary>
        ///     Retrieve a CarrierAccount from its id.
        /// </summary>
        /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        [CrudOperations.Read]
        public async Task<CarrierAccount> Retrieve(string id) => await Get<CarrierAccount>($"carrier_accounts/{id}");
        
        /// <summary>
        ///     Update this CarrierAccount.
        /// </summary>
        /// <param name="parameters">See CarrierAccount.Create for more details.</param>
        /// <returns>The updated CarrierAccount.</returns>
        [CrudOperations.Update]
        public async Task<CarrierAccount> Update(string id, Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("carrier_account");
            return await Request<CarrierAccount>(Http.Method.Patch, $"carrier_accounts/{id}", parameters);
        }

        /// <summary>
        ///     Update this <see cref="CarrierAccount"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.CarrierAccounts.Update"/> parameter set.</param>
        /// <returns>This updated <see cref="CarrierAccount"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<CarrierAccount> Update(string id, BetaFeatures.Parameters.CarrierAccounts.Update parameters)
        {
            return await Request<CarrierAccount>(Http.Method.Patch, $"carrier_accounts/{id}", parameters.ToDictionary());
        }

        /// <summary>
        ///     Remove this CarrierAccount from your account.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        [CrudOperations.Delete]
        public async Task Delete(string id) => await DeleteNoResponse($"carrier_accounts/{id}");


        #endregion

        private static string SelectCarrierAccountCreationEndpoint(string carrierAccountType)
        {
            // endpoint will always be something since the switch case's default value will kick in,
            // but we have to initialize the variable to avoid a compiler nullability error
            string endpoint = string.Empty;

            SwitchCase @switch = new()
            {
                { Constants.CarrierAccountTypes.CarrierTypesWithCustomWorkflows.Contains(carrierAccountType), () => endpoint = "carrier_accounts/register" },
                { SwitchCaseScenario.Default, () => endpoint = "carrier_accounts" },
            };
            @switch.MatchFirstTrue();

            return endpoint;
        }
    }
}
