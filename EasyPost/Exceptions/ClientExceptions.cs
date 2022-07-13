using System;
using EasyPost.Clients;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class ClientNotConfiguredException : BaseException
    {
        public static string MessageTemplate => "Client is not configured.";

        internal ClientNotConfiguredException() : base(PopulateMessage(MessageTemplate))
        {
        }

        internal ClientNotConfiguredException(Exception innerException) : base(PopulateMessage(MessageTemplate), innerException)
        {
        }
    }

    [Serializable]
    internal class ApiVersionNotSupportedException : BaseException
    {
        public static string MessageTemplate => "{0} incompatible with API version {1}.";

        internal ApiVersionNotSupportedException(string elementName, ApiVersionDetails apiVersionDetails) : base(PopulateMessage(MessageTemplate, elementName, apiVersionDetails.Prefix))
        {
        }

        internal ApiVersionNotSupportedException(Exception innerException, string elementName, ApiVersionDetails apiVersionDetails) : base(PopulateMessage(MessageTemplate, elementName, apiVersionDetails.Prefix), innerException)
        {
        }
    }
}
