using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API.Beta;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services.Beta
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CarrierMetadataService : EasyPostService
    {
        internal CarrierMetadataService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Retrieve metadata about specific carrier(s).
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Beta.CarrierMetadata.Retrieve"/> parameter set.</param>
        /// <returns>A list of <see cref="Carrier"/> objects.</returns>
        [CrudOperations.Read]
        public async Task<List<Carrier>> RetrieveCarrierMetadata(BetaFeatures.Parameters.Beta.CarrierMetadata.Retrieve? parameters = null)
        {
            Dictionary<string, object> data = parameters?.ToDictionary() ?? new Dictionary<string, object>();

            return await RequestAsync<List<Carrier>>(Method.Get, "metadata", data, "carriers", ApiVersion.Beta);
        }

        #endregion
    }
}
