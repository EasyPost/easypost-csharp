using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ApiKey : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("key")]
        public string? Key { get; set; }

        #endregion

        internal ApiKey()
        {
        }
    }

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

        protected internal override TParameters BuildNextPageParameters<TEntries, TParameters>(IEnumerable<TEntries> entries, int? pageSize = null) => throw new System.NotImplementedException();
    }
}
