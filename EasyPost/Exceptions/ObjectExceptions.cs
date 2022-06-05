using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class ObjectException : Exception
    {
        internal ObjectException(string message) : base(message)
        {
        }
    }

    [Serializable]
    internal class PropertyMissing : ObjectException
    {
        internal PropertyMissing(string property) : base($"Missing {property}")
        {
        }
    }
}
