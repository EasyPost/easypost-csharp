using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class ServerObjectAlreadyExists : Exception
    {
        internal ServerObjectAlreadyExists() : base("This object already exists on the server.")
        {
        }
    }

    [Serializable]
    internal class ServerObjectDoesNotExists : Exception
    {
        internal ServerObjectDoesNotExists() : base("This object does not exist on the server.")
        {
        }
    }

    [Serializable]
    internal class ServerLocalObjectMismatch : Exception
    {
        internal ServerLocalObjectMismatch() : base("The local object and the server object are not the same.")
        {
        }
    }
}
