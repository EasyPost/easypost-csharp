using System;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class FilterFailureException : BaseException
    {
        internal FilterFailureException(string message) : base(message)
        {
        }

        internal FilterFailureException(Exception innerException, string message) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class PaymentNotSetUpException : BaseException
    {
        internal PaymentNotSetUpException(string message) : base(message)
        {
        }

        internal PaymentNotSetUpException(Exception innerException, string message) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class InvalidOptionException : BaseException
    {
        public static string MessageTemplate => "{0} is not a valid {1} option.";

        internal InvalidOptionException(string selectedOption, string? optionType = "") : base(PopulateMessage(MessageTemplate, selectedOption, optionType))
        {
        }

        internal InvalidOptionException(Exception innerException, string selectedOption, string? optionType = "") : base(PopulateMessage(MessageTemplate, selectedOption, optionType), innerException)
        {
        }
    }
}
