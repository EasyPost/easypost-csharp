using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API.Beta;
using EasyPost.Utilities.Internal.Annotations;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services.Beta
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RateService : EasyPostService
    {
        internal RateService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Retrieve ephemeral rates for a shipment.
        ///
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing shipment data. Valid pairs:
        ///     * {"from_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"to_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"buyer_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"return_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"parcel", Dictionary&lt;string, object&gt;} See Parcel.Create for list of valid keys.
        ///     * {"customs_info", Dictionary&lt;string, object&gt;} See CustomsInfo.Create for list of valid keys.
        ///     * {"options", Dictionary&lt;string, object&gt;} See https://www.easypost.com/docs/api#shipments for list of
        ///     options.
        ///     * {"is_return", bool}
        ///     * {"currency", string} Defaults to "USD".
        ///     * {"reference", string}
        ///     * {"carrier_accounts", List&lt;string&gt;} List of CarrierAccount.id to limit rating.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>A list of <see cref="StatelessRate"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<List<EasyPost.Models.API.Beta.StatelessRate>> RetrieveStatelessRates(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("shipment");
            return await Create<List<EasyPost.Models.API.Beta.StatelessRate>>("rates", parameters, "rates", ApiVersion.Beta);
        }

        #endregion
    }
}
