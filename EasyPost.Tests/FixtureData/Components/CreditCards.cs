using System.Collections.Generic;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData.Components
{
    public class CreditCards
    {
        #region JSON Properties

        [JsonProperty("test")]
        public Dictionary<string, object> Test { get; set; }

        #endregion
    }
}
