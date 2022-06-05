using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class ClientNotConfigured : Exception
    {
        internal ClientNotConfigured() : base("Client is not configured.")
        {
        }
    }
}
