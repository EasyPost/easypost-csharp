using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API;

public class ScanForm : EasyPostObject
{
    #region JSON Properties

    [JsonProperty("address")]
    public Address? Address { get; set; }
    [JsonProperty("batch_id")]
    public string? BatchId { get; set; }
    [JsonProperty("form_file_type")]
    public string? FormFileType { get; set; }
    [JsonProperty("form_url")]
    public string? FormUrl { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("tracking_codes")]
    public List<string>? TrackingCodes { get; set; }

    #endregion

    internal ScanForm()
    {
    }
}

public class ScanFormCollection : Collection
{
    #region JSON Properties

    [JsonProperty("scan_forms")]
    public List<ScanForm>? ScanForms { get; set; }

    #endregion

    internal ScanFormCollection()
    {
    }
}
