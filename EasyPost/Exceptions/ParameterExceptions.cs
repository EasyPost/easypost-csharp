using System;
using System.Reflection;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class MissingRequiredParameterException : BaseException
    {
        public static string MessageTemplate => "{0} is a required parameter.";

        internal MissingRequiredParameterException(PropertyInfo property) : base(PopulateMessage(MessageTemplate, property.Name))
        {
        }

        internal MissingRequiredParameterException(Exception innerException, PropertyInfo property) : base(PopulateMessage(MessageTemplate, property.Name), innerException)
        {
        }
    }
}
