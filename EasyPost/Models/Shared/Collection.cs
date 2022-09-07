using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.Shared
{
    public class Collection : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("has_more")]
        public bool? HasMore { get; set; }

        [JsonProperty("filters")]
        internal Dictionary<string, object>? Filters { get; set; }

        #endregion
    }
}
