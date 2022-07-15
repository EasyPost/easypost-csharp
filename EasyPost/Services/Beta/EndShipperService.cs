using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.API.Beta;
using EasyPost.Parameters;
using EasyPost.Parameters.Beta;

namespace EasyPost.Services.Beta
{
    public class EndShipperService : EasyPostService
    {
        internal EndShipperService(Client client) : base(client)
        {
        }

        /// <summary>
        ///     List all EndShipper objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing an EndShipper ID. Starts with "es_". Only retrieve EndShippers created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an EndShipper ID. Starts with "es". Only retrieve EndShippers created
        ///     after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve EndShippers created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve EndShippers created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>A list of EasyPost.EndShipper instances.</returns>
        [ApiCompatibility(ApiVersion.Beta)]
        public async Task<List<EndShipper>> All(All? parameters = null) => await List<List<EndShipper>>("end_shippers", parameters);

        /// <summary>
        ///     Create an EndShipper.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the EndShipper with. Valid pairs:
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
        /// <returns>EasyPost.EndShipper instance.</returns>
        [ApiCompatibility(ApiVersion.Beta)]
        public async Task<EndShipper> Create(EndShippers.Create? parameters = null)
        {
            return await Create<EndShipper>("end_shippers", parameters);
        }

        /// <summary>
        ///     Retrieve an EndShipper from its id.
        /// </summary>
        /// <param name="id">String representing an EndShipper. Starts with "es_".</param>
        /// <returns>EasyPost.EndShipper instance.</returns>
        [ApiCompatibility(ApiVersion.Beta)]
        public async Task<EndShipper> Retrieve(string id) => await Get<EndShipper>($"end_shippers/{id}");
    }
}
