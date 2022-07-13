using System.Collections.Generic;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class RefundCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("refunds")]
        public List<Refund>? Refunds { get; set; }

        #endregion
    }
}
