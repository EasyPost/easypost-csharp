using Newtonsoft.Json;

#pragma warning disable CS8618

namespace EasyPost.Tests.FixtureData.Components
{
    public class ServiceNames
    {
        #region JSON Properties

        [JsonProperty("usps")]
        public ServiceNamesUsps Usps { get; set; }

        #endregion
    }

    public class ServiceNamesUsps
    {
        #region JSON Properties

        [JsonProperty("first_service")]
        public string FirstService { get; set; }

        [JsonProperty("pickup_service")]
        public string PickupService { get; set; }

        #endregion
    }
}
