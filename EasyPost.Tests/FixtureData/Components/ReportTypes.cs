using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData.Components
{
    public class ReportTypes
    {
        #region JSON Properties

        [JsonProperty("shipment")]
        public string Shipment { get; set; }

        #endregion
    }
}
