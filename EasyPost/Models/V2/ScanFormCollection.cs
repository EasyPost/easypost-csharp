using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Exceptions;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ScanFormCollection : Collection, IPaginatedCollection
    {
        [JsonProperty("scan_forms")]
        public List<ScanForm>? scan_forms { get; set; }

        /// <summary>
        ///     Get the next page of scan forms based on the original parameters passed to ScanForm.All().
        /// </summary>
        /// <returns>An EasyPost.ScanFormCollection instance.</returns>
        public async Task<IPaginatedCollection> Next()
        {
            filters ??= new Dictionary<string, object>();
            filters["before_id"] = (scan_forms ?? throw new PropertyMissing("scan_forms")).Last().id ?? throw new PropertyMissing("id");

            if (Client == null)
            {
                throw new ClientNotConfigured();
            }

            return await Client.ScanForms.All(filters);
        }
    }
}
