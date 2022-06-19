using System;

namespace EasyPost.Clients
{
    /// <summary>
    ///     Available EasyPost API versions.
    /// </summary>
    public enum ApiVersion
    {
        V2,
        Latest,
        Beta
    }

    internal abstract class ApiVersionDetails
    {
        /// <summary>
        ///     Derive the API version string from the enum.
        /// </summary>
        /// <param name="apiVersion">API version enum.</param>
        /// <returns>Corresponding API version string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Enum is invalid.</exception>
        internal static string FromEnum(ApiVersion apiVersion)
        {
            switch (apiVersion)
            {
                case ApiVersion.V2:
                case ApiVersion.Latest:
                    return "v2";
                case ApiVersion.Beta:
                    return "beta";
                default:
                    throw new ArgumentOutOfRangeException(nameof(apiVersion), apiVersion, null);
            }
        }
    }
}
