using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ScanFormCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("scan_forms")]
        public List<ScanForm>? ScanForms { get; set; }

        #endregion
    }
}
