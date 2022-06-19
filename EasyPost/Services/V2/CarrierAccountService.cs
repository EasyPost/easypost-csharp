using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Services.V2
{
    public class CarrierAccountService : Service
    {
        internal CarrierAccountService(Client client) : base(client)
        {
        }


        /// <summary>
        ///     List all available carrier accounts.
        /// </summary>
        /// <returns>A list of EasyPost.CarrierAccount instances.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<List<CarrierAccount>> All()
        {
            return await List<List<CarrierAccount>>("carrier_accounts");
        }

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
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<CarrierAccount> Create(Dictionary<string, object> parameters)
        {
            return await Create<CarrierAccount>("carrier_accounts", new Dictionary<string, object>
            {
                {
                    "carrier_account", parameters
                }
            });
        }

        /// <summary>
        ///     Retrieve a CarrierAccount from its id.
        /// </summary>
        /// <param name="id">String representing a carrier account. Starts with "ca_".</param>
        /// <returns>EasyPost.CarrierAccount instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<CarrierAccount> Retrieve(string id)
        {
            return await Get<CarrierAccount>($"carrier_accounts/{id}");
        }
    }
}
