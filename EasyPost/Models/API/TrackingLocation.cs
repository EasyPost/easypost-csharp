using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API;

public class TrackingLocation : EasyPostObject
{
    #region JSON Properties

    [JsonProperty("city")]
    public string? City { get; set; }
    [JsonProperty("country")]
    public string? Country { get; set; }
    [JsonProperty("state")]
    public string? State { get; set; }
    [JsonProperty("zip")]
    public string? Zip { get; set; }

    #endregion

    internal TrackingLocation()
    {
    }
}
