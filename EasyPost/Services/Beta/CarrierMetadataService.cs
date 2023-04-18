using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
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

        [CrudOperations.Read]
        public async Task<List<Carrier>> RetrieveCarrierMetadata(List<string>? carriers = null, List<CarrierMetadataType>? types = null)
        {
            var parameters = new Dictionary<string, object>();

            if (carriers != null)
            {
                parameters.Add("carriers", string.Join(",", carriers));
            }

            if (types != null)
            {
                parameters.Add("types", string.Join(",", types.Select(type => type.ToString())));
            }

            return await Get<List<Carrier>>("metadata", parameters, "carriers", ApiVersion.Beta);
        }

        #endregion
    }
}
