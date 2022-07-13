using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class ServerSideConfigurationException : BaseException
    {
        public static string MessageTemplate => "{0} is not configured on the server. {1}";

        internal ServerSideConfigurationException(string element, string? followup = "") : base(PopulateMessage(MessageTemplate, element, followup))
        {
        }

        internal ServerSideConfigurationException(Exception innerException, string element, string? followup = "") : base(PopulateMessage(MessageTemplate, element, followup), innerException)
        {
        }
    }
}
