using System.Collections.Generic;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData.Components
{
    public class FormOptions
    {
        #region JSON Properties

        [JsonProperty("rma")]
        public Dictionary<string, object> Rma { get; set; }

        #endregion
    }
}
