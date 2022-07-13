using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class ObjectException : BaseException
    {
        internal ObjectException(string message) : base(message)
        {
        }

        internal ObjectException(Exception innerException, string message) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class PropertyMissingException : ObjectException
    {
        internal PropertyMissingException(string property) : base($"Missing {property}")
        {
        }

        internal PropertyMissingException(Exception innerException, string message) : base(innerException, message)
        {
        }
    }
}
