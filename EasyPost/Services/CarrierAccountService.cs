using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CarrierAccountService : EasyPostService
    {
        internal CarrierAccountService(EasyPostClient client) : base(client)
        {
        }

        #region CRUD Operations

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
                throw new MissingParameterError("CarrierAccount type is required.");

            string endpoint = GenerateEndpoint(carrierType);

            parameters = parameters.Wrap("carrier_account");

            return await Create<CarrierAccount>(endpoint, parameters);
        }

        /// <summary>
        ///     List all available carrier accounts.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierAccount instances.</returns>
        [CrudOperations.Read]
        public async Task<List<CarrierAccount>> All()
        {
            return await List<List<CarrierAccount>>("carrier_accounts");
        }

        /// <summary>
        ///     Retrieve a CarrierAccount from its id.
        /// </summary>
        /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        [CrudOperations.Read]
        public async Task<CarrierAccount> Retrieve(string id)
        {
            return await Get<CarrierAccount>($"carrier_accounts/{id}");
        }

        #endregion

        private static string GenerateEndpoint(string carrierAccountType)
        {
            var registerEndpointCarriers = new List<string>
            {
                "UpsAccount",
                "FedexAccount"
            };

            string endpoint = "carrier_accounts";

            var @switch = new SwitchCase
            {
                { registerEndpointCarriers.Contains(carrierAccountType), () => endpoint = "carrier_accounts/register" },
                { SwitchCaseScenario.Default, () => endpoint = "carrier_accounts" }
            };
            @switch.MatchFirstTrue();

            return endpoint;
        }
    }
}
