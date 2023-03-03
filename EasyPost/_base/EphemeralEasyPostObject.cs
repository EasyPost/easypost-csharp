using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities;
using Newtonsoft.Json;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    /// <summary>
    ///     Class for any ephemeral (non-permanent) object that comes from or goes to the EasyPost API.
    /// </summary>
    public abstract class EphemeralEasyPostObject : WithClient, IEasyPostObject
    {
        #region JSON Properties

        [JsonProperty("mode")]
        public string? Mode { get; internal set; }
        [JsonProperty("object")]
        internal string? Object { get; set; }

        #endregion

        public override bool Equals(object? obj) => GetType() == obj?.GetType() && GetHashCode() == ((EphemeralEasyPostObject)obj).GetHashCode();

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode", Justification = "Client is used to determine equality.")]
#pragma warning disable CA1307
        public override int GetHashCode() => AsJson().GetHashCode() ^ GetType().GetHashCode() ^ (Client != null ? Client!.GetHashCode() : 1);
#pragma warning restore CA1307

        public static bool operator ==(EphemeralEasyPostObject? one, object? two)
        {
            if (one is null && two is null)
            {
                return true;
            }

#pragma warning disable IDE0046
            if (one is null || two is null)
#pragma warning restore IDE0046
            {
                return false;
            }

            return one.Equals(two);
        }

        public static bool operator !=(EphemeralEasyPostObject? one, object? two) => !(one == two);

        /// <summary>
        ///     Get the JSON representation of this object instance.
        /// </summary>
        /// <returns>A JSON string representation of this object instance's attributes.</returns>
        protected string AsJson() => JsonSerialization.ConvertObjectToJson(this);
    }

    public interface IEasyPostObject
    {
    }
}
