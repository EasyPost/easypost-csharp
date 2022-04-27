using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class ScanFormCollection : Collection
    {
        [JsonProperty("scan_forms")]
        public List<ScanForm> scan_forms { get; set; }

        /// <summary>
        ///     Get the next page of scan forms based on the original parameters passed to ScanForm.All().
        /// </summary>
        /// <returns>An EasyPost.ScanFormCollection instance.</returns>
        public async Task<ScanFormCollection> Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = scan_forms.Last().id;

            if (Client == null)
            {
                throw new Exception("Client is null");
            }

            return await Client.ScanForms.All(filters);
        }
    }
}
