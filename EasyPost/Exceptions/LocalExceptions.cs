using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class FilterFailure : Exception
    {
        internal FilterFailure(string message) : base(message)
        {
        }
    }
}
