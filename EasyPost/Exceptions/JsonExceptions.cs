using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class BaseJsonException : BaseException
    {
        internal BaseJsonException(string message) : base(message)
        {
        }

        internal BaseJsonException(Exception innerException, string message) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class DeserializationException : BaseJsonException
    {
        internal DeserializationException(string message) : base(message)
        {
        }

        internal DeserializationException(Exception innerException, string message) : base(innerException, message)
        {
        }
    }

    [Serializable]
    internal class SerializationException : BaseJsonException
    {
        internal SerializationException(string message) : base(message)
        {
        }

        internal SerializationException(Exception innerException, string message) : base(innerException, message)
        {
        }
    }
}
