using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost.Beta
{
    public class EndShipper : Base.Address
    {
        /// <summary>
        ///     Update this EndShipper. Must pass in all properties (new and existing).
        /// </summary>
        /// <param name="parameters">See EndShipper.Create for more details.</param>
        public async Task Update(Dictionary<string, object> parameters)
        {
            Request request = new Request($"end_shippers/{id}", Method.Put);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "address", parameters
                }
            });

            Merge(await request.Execute<EndShipper>(true));
        }

        /// <summary>
        ///     List all EndShippers.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing an Address ID. Starts with "es_". Only retrieve EndShippers created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an Address ID. Starts with "es_". Only retrieve EndShippers created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve EndShippers created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve EndShippers created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>A list of EasyPost.EndShipper instances.</returns>
        public static async Task<List<EndShipper>> All(Dictionary<string, object>? parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();
            Request request = new Request("end_shippers", Method.Get, parameters);

            return await request.Execute<List<EndShipper>>(true);
        }

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
        public static async Task<EndShipper> Create(Dictionary<string, object>? parameters = null)
        {
            Request request = new Request("end_shippers", Method.Post);
            parameters ??= new Dictionary<string, object>();
            Dictionary<string, object> body = new Dictionary<string, object>();

            body.Add("address", parameters);

            request.AddParameters(body);

            return await request.Execute<EndShipper>(true);
        }

        /// <summary>
        ///     Retrieve an EndShipper from its id.
        /// </summary>
        /// <param name="id">String representing an EndShippers. Starts with "es_".</param>
        /// <returns>EasyPost.EndShipper instance.</returns>
        public static async Task<EndShipper> Retrieve(string id)
        {
            Request request = new Request($"end_shippers/{id}", Method.Get);
            // request.AddUrlSegment("id", id);

            return await request.Execute<EndShipper>(true);
        }
    }
}
