using System.Collections.Generic;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData.Components
{
    public class Addresses
    {
        #region JSON Properties

        [JsonProperty("ca_address_1")]
        public Dictionary<string, object> CaAddress1 { get; set; }

        [JsonProperty("ca_address_2")]
        public Dictionary<string, object> CaAddress2 { get; set; }

        [JsonProperty("incorrect")]
        public Dictionary<string, object> IncorrectAddress { get; set; }

        #endregion
    }
}
