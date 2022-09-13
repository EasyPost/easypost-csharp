using System.Collections.Generic;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData.Components
{
    public class TaxIdentifiers
    {
        #region JSON Properties

        [JsonProperty("basic")]
        public Dictionary<string, object> Basic { get; set; }

        #endregion
    }
}
