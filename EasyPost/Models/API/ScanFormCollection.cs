using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ScanFormCollection : Collection, IPaginatedCollection
    {
        #region JSON Properties

        [JsonProperty("filters")]
        public Dictionary<string, object>? filters { get; set; }
        [JsonProperty("scan_forms")]
        public List<ScanForm> scan_forms { get; set; }

        #endregion

        #region CRUD Operations

        /// <summary>
        ///     Get the next page of scan forms based on the original parameters passed to ScanForm.All().
        /// </summary>
        /// <returns>An EasyPost.ScanFormCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<IPaginatedCollection> Next()
        {
            UpdateFilters(scan_forms, "scan_forms");
            return await (Client as Client)!.ScanForm.All(Filters);
        }

        #endregion
    }
}
