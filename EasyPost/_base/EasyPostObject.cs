using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EasyPost.Utilities;
using Newtonsoft.Json;

namespace EasyPost._base;

/// <summary>
///     Class for any object that comes from or goes to the EasyPost API.
/// </summary>
public abstract class EasyPostObject : IEasyPostObject
{
    #region JSON Properties

    [JsonProperty("created_at")]
    public DateTime? CreatedAt { get; internal set; }
    [JsonProperty("id")]
    public string? Id { get; internal set; }
    [JsonProperty("mode")]
    public string? Mode { get; internal set; }
    [JsonProperty("updated_at")]
    public DateTime? UpdatedAt { get; internal set; }
    [JsonProperty("object")]
    internal string? Object { get; set; }

    #endregion

    internal string? Prefix => Id?.Split('_').First();

    public override bool Equals(object? obj)
    {
        if (GetType() != obj?.GetType())
        {
            return false;
        }

        return GetHashCode() == ((EasyPostObject)obj).GetHashCode();
    }

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode() => AsJson().GetHashCode() ^ GetType().GetHashCode();

    /// <summary>
    ///     Get the JSON representation of this object instance.
    /// </summary>
    /// <returns>A JSON string representation of this object instance's attributes</returns>
    private string AsJson() => JsonSerialization.ConvertObjectToJson(this);

    public static bool operator ==(EasyPostObject? one, object? two)
    {
        if (one is null && two is null)
        {
            return true;
        }

        if (one is null || two is null)
        {
            return false;
        }

        return one.Equals(two);
    }

    public static bool operator !=(EasyPostObject? one, object? two) => !(one == two);
}

public interface IEasyPostObject
{
}
