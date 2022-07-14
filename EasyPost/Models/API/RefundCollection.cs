using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class RefundCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("refunds")]
        public List<Refund>? Refunds { get; set; }

        #endregion
    }
}
