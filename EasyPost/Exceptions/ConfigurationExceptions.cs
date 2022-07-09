using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class ServerSideConfigurationException : Exception
    {
        internal ServerSideConfigurationException(string element, string? followup = "") : base($"{element} is not configured on the server. {followup}")
        {
        }
    }
}
