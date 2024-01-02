using EasyPost.Utilities.Internal;

#pragma warning disable SA1300
namespace EasyPost._base
#pragma warning restore SA1300
{
    /// <summary>
    ///     Enums for the version of the API to target when making requests.
    /// </summary>
    public class ApiVersion : ValueEnum
    {
        /// <summary>
        ///     The beta version of the API.
        /// </summary>
        public static readonly ApiVersion Beta = new(2, "beta");

        /// <summary>
        ///     Version 2 of the API.
        /// </summary>
        public static readonly ApiVersion V2 = new(1, "v2");

        /// <summary>
        ///     The current (general availability) version of the API.
        /// </summary>
        public static readonly ApiVersion Current = V2;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiVersion"/> class.
        /// </summary>
        /// <param name="id">Internal ID for the enum.</param>
        /// <param name="value">String representation of the API version, used when crafting HTTP URLs.</param>
        private ApiVersion(int id, object value)
            : base(id, value)
        {
        }
    }
}
