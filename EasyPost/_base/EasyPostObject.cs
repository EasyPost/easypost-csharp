using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        ///     The date and time this object was created.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; internal set; }

        /// <summary>
        ///     The EasyPost ID for this object.
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        ///     The date and time this object was last updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }

        #endregion

        /// <summary>
        ///     The prefix of the EasyPost ID for this object.
        /// </summary>
        public string? Prefix => Id?.Split('_').First();

        /// <summary>
        ///     Gets this object as a JSON object (dictionary).
        /// </summary>
        /// <returns>A dictionary representation of this object's properties.</returns>
        internal Dictionary<string, object> AsDictionary() => JsonConvert.DeserializeObject<Dictionary<string, object>>(AsJson())!;

        /// <inheritdoc />
        public override bool Equals(object? obj) => GetType() == obj?.GetType() && GetHashCode() == ((EasyPostObject)obj).GetHashCode();

        /// <inheritdoc />
        // ReSharper disable once NonReadonlyMemberInGetHashCode
#pragma warning disable CA1307
        public override int GetHashCode() => base.GetHashCode() ^ Id!.GetHashCode();
#pragma warning restore CA1307

        /// <inheritdoc cref="EphemeralEasyPostObject.operator =="/>
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

        /// <inheritdoc cref="EphemeralEasyPostObject.operator !="/>
        public static bool operator !=(EasyPostObject? one, object? two) => !(one == two);
    }
}
