using System;
using EasyPost.Clients;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class ClientNotConfigured : Exception
    {
        internal ClientNotConfigured() : base("Client is not configured.")
        {
        }
    }

    [Serializable]
    internal class ApiVersionNotSupported : Exception
    {
        internal ApiVersionNotSupported(string elementName, ApiVersionDetails apiVersionDetails) : base($"{elementName} incompatible with API version {apiVersionDetails.Prefix}.")
        {
        }
    }
}
