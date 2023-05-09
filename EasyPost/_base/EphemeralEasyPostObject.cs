using EasyPost.Utilities.Internal;
using Newtonsoft.Json;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    /// <summary>
    ///     Class for any ephemeral (non-permanent) object that comes from or goes to the EasyPost API.
    /// </summary>
    public abstract class EphemeralEasyPostObject : IEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The API mode used when interacting with this object. Can be <c>test</c> or <c>production</c>.
        /// </summary>
        [JsonProperty("mode")]
        public string? Mode { get; internal set; }

        /// <summary>
        ///     The type of EasyPost data structure this object represents.
        /// </summary>
        [JsonProperty("object")]
        internal string? Object { get; set; }

        #endregion

        /// <inheritdoc />
        public override bool Equals(object? obj) => GetType() == obj?.GetType() && GetHashCode() == ((EphemeralEasyPostObject)obj).GetHashCode();
        
        /// <inheritdoc />
#pragma warning disable CA1307 // Specify StringComparison
        public override int GetHashCode() => AsJson().GetHashCode() ^ GetType().GetHashCode();
#pragma warning restore CA1307 // Specify StringComparison

        /// <summary>
        ///     Compare two objects for equality.
        /// </summary>
        /// <param name="one">The first object in the comparison.</param>
        /// <param name="two">The second object in the comparison.</param>
        /// <returns><c>true</c> if the two objects are equal; otherwise, false.</returns>
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

        /// <summary>
        ///     Compare two objects for equality.
        /// </summary>
        /// <param name="one">The first object in the comparison.</param>
        /// <param name="two">The second object in the comparison.</param>
        /// <returns><c>true</c> if the two objects are not equal; otherwise, false.</returns>
        public static bool operator !=(EphemeralEasyPostObject? one, object? two) => !(one == two);

        /// <summary>
        ///     Get the JSON representation of this object instance.
        /// </summary>
        /// <returns>A JSON string representation of this object instance's attributes.</returns>
        protected string AsJson() => JsonSerialization.ConvertObjectToJson(this);
    }

    /// <summary>
    ///     Interface for any object that represents a data structure from the EasyPost API.
    /// </summary>
    public interface IEasyPostObject
    {
    }
}
