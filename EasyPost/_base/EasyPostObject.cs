using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    /// <summary>
    ///     Class for any object that comes from or goes to the EasyPost API.
    /// </summary>
    public abstract class EasyPostObject : EphemeralEasyPostObject
    {
        #region JSON Properties

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; internal set; }
        [JsonProperty("id")]
        public string? Id { get; internal set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }

        #endregion

        internal string? Prefix => Id?.Split('_').First();

        internal Dictionary<string, object> AsDictionary() => JsonConvert.DeserializeObject<Dictionary<string, object>>(AsJson())!;

        public override bool Equals(object? obj) => GetType() == obj?.GetType() && GetHashCode() == ((EasyPostObject)obj).GetHashCode();

#pragma warning disable CA1307
        public override int GetHashCode() => AsJson().GetHashCode() ^ GetType().GetHashCode();
#pragma warning restore CA1307

        public static bool operator ==(EasyPostObject? one, object? two)
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

        public static bool operator !=(EasyPostObject? one, object? two) => !(one == two);
    }
}
