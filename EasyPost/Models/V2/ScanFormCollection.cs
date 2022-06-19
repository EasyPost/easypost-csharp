using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ScanFormCollection : Collection, IPaginatedCollection
    {
        [JsonProperty("scan_forms")]
        public List<ScanForm>? ScanForms { get; set; }

        /// <summary>
        ///     Get the next page of scan forms based on the original parameters passed to ScanForm.All().
        /// </summary>
        /// <returns>An EasyPost.ScanFormCollection instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<IPaginatedCollection> Next()
        {
            Filters ??= new Dictionary<string, object>();
            Filters["before_id"] = (ScanForms ?? throw new PropertyMissing("scan_forms")).Last().Id ?? throw new PropertyMissing("id");

            if (Client == null)
            {
                throw new ClientNotConfigured();
            }

            return await Client.ScanForms.All(Filters);
        }
    }
}
