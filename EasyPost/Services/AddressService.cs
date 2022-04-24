using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models;

namespace EasyPost.Services
{
    public class AddressService : Service
    {
        public AddressService(ApiClient client) : base(client)
        {
        }

        /// <summary>
        ///     Create an Address.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the address with. Valid pairs:
        ///     * {"name", string}
        ///     * {"company", string}
        ///     * {"street1", string}
        ///     * {"street2", string}
        ///     * {"city", string}
        ///     * {"state", string}
        ///     * {"zip", string}
        ///     * {"country", string}
        ///     * {"phone", string}
        ///     * {"email", string}
        ///     * {"verifications", List&lt;string&gt;} Possible items are "delivery" and "zip4".
        ///     * {"strict_verifications", List&lt;string&gt;} Possible items are "delivery" and "zip4".
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Address instance.</returns>
        public async Task<Address> Create(Dictionary<string, object>? parameters = null)
        {
            return await SendCreate("addresses", parameters);
        }

        /// <summary>
        ///     Create and verify an Address.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the address with. Valid pairs:
        ///     * {"name", string}
        ///     * {"company", string}
        ///     * {"street1", string}
        ///     * {"street2", string}
        ///     * {"city", string}
        ///     * {"state", string}
        ///     * {"zip", string}
        ///     * {"country", string}
        ///     * {"phone", string}
        ///     * {"email", string}
        ///     All invalid keys will be ignored.
        /// </param>
        public async Task<Address> CreateAndVerify(Dictionary<string, object>? parameters = null)
        {
            return await SendCreate("addresses/create_and_verify", parameters, "address");
        }

        /// <summary>
        ///     Retrieve an Address from its id.
        /// </summary>
        /// <param name="id">String representing an Address. Starts with "adr_".</param>
        /// <returns>EasyPost.Address instance.</returns>
        public async Task<Address> Retrieve(string id)
        {
            return await Get<Address>($"addresses/{id}");
        }

        /// <summary>
        ///     List all Address objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing an Address ID. Starts with "adr_". Only retrieve addresses created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an Address ID. Starts with "adr". Only retrieve addresses created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve addresses created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve addresses created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.AddressCollection instance.</returns>
        public async Task<AddressCollection> All(Dictionary<string, object>? parameters = null)
        {
            return await List<AddressCollection>("addresses", parameters);
        }

        private async Task<Address> SendCreate(string endpoint, Dictionary<string, object>? parameters = null, string? rootElement = null)
        {
            parameters ??= new Dictionary<string, object>();
            Dictionary<string, object> body = new Dictionary<string, object>();

            if (parameters.ContainsKey("verify"))
            {
                body.Add("verify", parameters["verify"]);
                // removing verify from the address data parameters, since it needs to be one key above
                parameters.Remove("verify");
            }

            if (parameters.ContainsKey("verify_strict"))
            {
                body.Add("verify_strict", parameters["verify_strict"]);
                // removing verify_strict from the address data parameters, since it needs to be one key above
                parameters.Remove("verify_strict");
            }

            body.Add("address", parameters);

            return await Create<Address>(endpoint, body, rootElement);
        }
    }
}
