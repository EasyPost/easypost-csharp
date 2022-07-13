using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class ServerObjectAlreadyExistsException : BaseException
    {
        public static string MessageTemplate => "This object already exists on the server.";

        internal ServerObjectAlreadyExistsException() : base(PopulateMessage(MessageTemplate))
        {
        }

        internal ServerObjectAlreadyExistsException(Exception innerException) : base(PopulateMessage(MessageTemplate), innerException)
        {
        }
    }

    [Serializable]
    internal class ServerObjectDoesNotExistsException : BaseException
    {
        public static string MessageTemplate => "This object does not exists on the server.";

        internal ServerObjectDoesNotExistsException() : base(PopulateMessage(MessageTemplate))
        {
        }

        internal ServerObjectDoesNotExistsException(Exception innerException) : base(PopulateMessage(MessageTemplate), innerException)
        {
        }
    }

    [Serializable]
    internal class ServerLocalObjectMismatchException : BaseException
    {
        public static string MessageTemplate => "The local object and the server object are not the same.";

        internal ServerLocalObjectMismatchException() : base(PopulateMessage(MessageTemplate))
        {
        }

        internal ServerLocalObjectMismatchException(Exception innerException) : base(PopulateMessage(MessageTemplate), innerException)
        {
        }
    }
}
