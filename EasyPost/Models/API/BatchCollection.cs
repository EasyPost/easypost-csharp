using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class BatchCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("batches")]
        public List<Batch> batches { get; set; }

        #endregion
    }
}
