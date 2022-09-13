using System.Collections.Generic;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData.Components
{
    public class Shipments
    {
        #region JSON Properties

        [JsonProperty("basic_domestic")]
        public Dictionary<string, object> BasicDomestic { get; set; }

        [JsonProperty("full")]
        public Dictionary<string, object> Full { get; set; }

        #endregion
    }
}
