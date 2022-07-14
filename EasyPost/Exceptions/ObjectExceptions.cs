using System;
using EasyPost.Clients;

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
        public static string MessageTemplate => "Missing {0}.";

        internal PropertyMissingException(string property) : base(PopulateMessage(MessageTemplate, property))
        {
        }

        internal PropertyMissingException(Exception innerException, string property) : base(innerException, PopulateMessage(MessageTemplate, property))
        {
        }
    }

    [Serializable]
    internal class BackwardsCompatibilityConversionException : ObjectException
    {
        public static string MessageTemplate => "Could not convert {0} into a {1}-compatible object.";

        internal BackwardsCompatibilityConversionException(object obj, ApiVersionDetails apiVersionDetails) : base(PopulateMessage(MessageTemplate, obj.GetType().Name, apiVersionDetails.Name))
        {
        }

        internal BackwardsCompatibilityConversionException(Exception innerException, object obj, ApiVersionDetails apiVersionDetails) : base(innerException, PopulateMessage(MessageTemplate, obj.GetType().Name, apiVersionDetails.Name))
        {
        }
    }
}
