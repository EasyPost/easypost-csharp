using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#api-key-object">EasyPost API key</a>.
    /// </summary>
    public class ApiKey : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The actual key value to use for authentication.
        /// </summary>
        [JsonProperty("key")]
        public string? Key { get; set; }

        #endregion
    }

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="ApiKey"/>s.
    /// </summary>
    public class ApiKeyCollection : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     A list of all child user's API keys.
        /// </summary>
        [JsonProperty("children")]
        public List<ApiKeyCollection>? Children { get; set; }

        /// <summary>
        ///     A lis of all API keys active for the current user's account.
        /// </summary>
        [JsonProperty("keys")]
        public List<ApiKey>? Keys { get; set; }

        #endregion
    }
}
