using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class ClientNotConfigured : Exception
    {
        internal ClientNotConfigured(string message) : base(message)
        {
        }
    }
}
