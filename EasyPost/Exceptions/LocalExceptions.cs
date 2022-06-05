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

    [Serializable]
    internal class PaymentNotSetUp : Exception
    {
        internal PaymentNotSetUp(string message) : base(message)
        {
        }
    }

    [Serializable]
    internal class InvalidOption : Exception
    {
        internal InvalidOption(string selectedOption, string? optionType = "") : base($"{selectedOption} is not a valid {optionType} option.")
        {
        }
    }
}
