using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API.Beta;
using EasyPost.Utilities.Internal.Attributes;
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
        public async Task<List<StatelessRate>> RetrieveStatelessRates(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("shipment");
            return await RequestAsync<List<StatelessRate>>(Method.Post, "rates", cancellationToken, parameters, "rates", ApiVersion.Beta);
        }

        /// <summary>
        ///     Retrieve ephemeral rates.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Beta.Rates.Retrieve"/> parameter set.</param>
        /// <returns>A list of <see cref="StatelessRate"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<List<StatelessRate>> RetrieveStatelessRates(BetaFeatures.Parameters.Beta.Rates.Retrieve parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<List<StatelessRate>>(Method.Post, "rates", cancellationToken, parameters.ToDictionary(), "rates", ApiVersion.Beta);
        }

        #endregion
    }
}
