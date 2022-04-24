using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models;
using RestSharp;

namespace EasyPost.Services
{
    public class Pickups : Service
    {
        public Pickups(ApiClient client) : base(client)
        {
        }

        /// <summary>
        ///     Create a Pickup.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///     * {"is_account_address", bool}
        ///     * {"min_datetime", DateTime}
        ///     * {"max_datetime", DateTime}
        ///     * {"reference", string}
        ///     * {"instructions", string}
        ///     * {"carrier_accounts", List&lt;CarrierAccount&gt;}
        ///     * {"address", Address}
        ///     * {"shipment", Shipment}
        ///     * {"batch", Batch}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public async Task<Pickup> Create(Dictionary<string, object> parameters)
        {
            return await Create<Pickup>("pickups", new Dictionary<string, object>
            {
                {
                    "pickup", parameters
                }
            });
        }


        /// <summary>
        ///     Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        public async Task<Pickup> Retrieve(string id)
        {
            return await Get<Pickup>($"pickups/{id}");
        }
    }
}
