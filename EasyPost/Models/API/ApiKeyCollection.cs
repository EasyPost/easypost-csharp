using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ApiKeyCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("children")]
        public List<ApiKeyCollection>? Children { get; set; }

        [JsonProperty("keys")]
        public List<ApiKey>? Keys { get; set; }

        #endregion

        internal ApiKeyCollection()
        {
        }
    }
}
