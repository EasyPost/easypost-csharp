using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API;

public class Event : EasyPostObject
{
    #region JSON Properties

    [JsonProperty("completed_urls")]
    public List<string>? CompletedUrls { get; set; }
    [JsonProperty("description")]
    public string? Description { get; set; }
    [JsonProperty("pending_urls")]
    public List<string>? PendingUrls { get; set; }
    [JsonProperty("previous_attributes")]
    public Dictionary<string, object>? PreviousAttributes { get; set; }
    [JsonProperty("result")]
    public Dictionary<string, object>? Result { get; set; }
    [JsonProperty("status")]
    public string? Status { get; set; }

    #endregion

    internal Event()
    {
    }
}

public class EventCollection : Collection
{
    #region JSON Properties

    [JsonProperty("events")]
    public List<Event>? Events { get; set; }

    #endregion

    internal EventCollection()
    {
    }
}
