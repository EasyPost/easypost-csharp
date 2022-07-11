using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ScanFormCollection : Collection, IPaginatedCollection
    {
        #region JSON Properties

        [JsonProperty("scan_forms")]
        public List<ScanForm>? ScanForms { get; set; }

        #endregion

        /// <summary>
        ///     Get the next page of scan forms based on the original parameters passed to ScanForm.All().
        /// </summary>
        /// <returns>An EasyPost.ScanFormCollection instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<IPaginatedCollection> Next()
        {
            UpdateFilters(ScanForms, "scan_forms");
            return await Client!.ScanForms.All(Filters);
        }
    }
}
