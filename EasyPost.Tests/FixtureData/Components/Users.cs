using System.Collections.Generic;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData.Components
{
    public class Users
    {
        #region JSON Properties

        [JsonProperty("referral")]
        public Dictionary<string, object> Referral { get; set; }

        #endregion
    }
}
