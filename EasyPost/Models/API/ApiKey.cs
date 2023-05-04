using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ApiKey : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("key")]
        public string? Key { get; internal set; }

        #endregion

        internal ApiKey()
        {
        }
    }

    public class ApiKeyCollection : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("children")]
        public List<ApiKeyCollection>? Children { get; internal set; }

        [JsonProperty("keys")]
        public List<ApiKey>? Keys { get; internal set; }

        #endregion

        internal ApiKeyCollection()
        {
        }
    }
}
