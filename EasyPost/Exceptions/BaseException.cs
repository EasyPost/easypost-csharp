using System;

namespace EasyPost.Exceptions
{
    internal abstract class BaseException : Exception
    {
        internal BaseException(string? message) : base(message)
        {
        }

        internal BaseException(string? message, Exception innerException) : base(message, innerException)
        {
        }

        internal static string PopulateMessage(string messageTemplate, params object?[]? elements)
        {
            return elements == null ? messageTemplate : string.Format(messageTemplate, elements);
        }
    }
}
