using System.Net;

namespace EasyPost.Models.API
{
    internal static class Security
    {
        /// <summary>
        ///     Get the TLS protocol version to use for the request.
        /// </summary>
        /// <returns>SecurityProtocolType TLS version enum.</returns>
        public static SecurityProtocolType GetProtocol()
        {
#if NET45
            return SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#else
            return (SecurityProtocolType)0x00000C00;
#endif
        }
    }
}
