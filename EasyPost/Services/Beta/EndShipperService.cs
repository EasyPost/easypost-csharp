using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models.Beta;

namespace EasyPost.Services.Beta
{
    public class EndShipperService : Service
    {
        internal EndShipperService(BaseClient client) : base(client)
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
        public async Task<List<EndShipper>> All(Dictionary<string, object>? parameters = null) => await List<List<EndShipper>>("end_shippers", parameters);

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
        public async Task<EndShipper> Create(Dictionary<string, object>? parameters = null)
        {
            parameters ??= new Dictionary<string, object>();
            Dictionary<string, object> body = new Dictionary<string, object>
            {
                {
                    "address", parameters
                }
            };

            return await Create<EndShipper>("end_shippers", body);
        }

        /// <summary>
        ///     Retrieve an EndShipper from its id.
        /// </summary>
        /// <param name="id">String representing an EndShipper. Starts with "es_".</param>
        /// <returns>EasyPost.EndShipper instance.</returns>
        public async Task<EndShipper> Retrieve(string id) => await Get<EndShipper>($"end_shippers/{id}");
    }
}
