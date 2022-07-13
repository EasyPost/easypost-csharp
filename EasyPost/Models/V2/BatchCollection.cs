using System.Collections.Generic;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class BatchCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("batches")]
        public List<Batch>? Batches { get; set; }

        #endregion
    }
}
