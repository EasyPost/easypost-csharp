using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class BaseJsonException : Exception
    {
        internal BaseJsonException(string message) : base(message)
        {
        }
    }

    [Serializable]
    internal class DeserializationException : BaseJsonException
    {
        internal DeserializationException(string message) : base(message)
        {
        }
    }

    [Serializable]
    internal class SerializationException : BaseJsonException
    {
        internal SerializationException(string message) : base(message)
        {
        }
    }
}
