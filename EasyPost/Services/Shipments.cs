using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyPost.Services
{
    public class Shipments : Service
    {
        public Shipments(ApiClient client) : base(client)
        {
        }

        /// <summary>
        ///     Create a Shipment.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the shipment with. Valid pairs:
        ///     * {"from_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"to_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"buyer_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"return_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"parcel", Dictionary&lt;string, object&gt;} See Parcel.Create for list of valid keys.
        ///     * {"customs_info", Dictionary&lt;string, object&gt;} See CustomsInfo.Create for lsit of valid keys.
        ///     * {"options", Dictionary&lt;string, object&gt;} See https://www.easypost.com/docs/api#shipments for list of
        ///     options.
        ///     * {"is_return", bool}
        ///     * {"currency", string} Defaults to "USD".
        ///     * {"reference", string}
        ///     * {"carrier_accounts", List&lt;string&gt;} List of CarrierAccount.id to limit rating.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.Shipment instance.</returns>
        public async Task<Shipment> Create(Dictionary<string, object>? parameters = null)
        {
            return await SendCreate(parameters);
        }

        private async Task<Shipment> SendCreate(Dictionary<string, object>? parameters = null)
        {
            parameters = new Dictionary<string, object>
            {
                {
                    "shipment", parameters ?? new Dictionary<string, object>()
                }
            };
            return await Create<Shipment>("shipments", parameters);
        }
    }
}
