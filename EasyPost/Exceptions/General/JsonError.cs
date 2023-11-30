using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace EasyPost.Exceptions.General
{
    /// <summary>
    ///     Base class for all JSON-related errors.
    /// </summary>
    public abstract class JsonError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonError" /> class.
        /// </summary>
        /// <param name="message">The error message to print to console.</param>
        internal JsonError(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Get a formatted error string with expanded details about the error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public override string PrettyPrint => Message;
    }

    /// <summary>
    ///     Represents an error that occurs while deserializing JSON.
    /// </summary>
    public class JsonDeserializationError : JsonError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonDeserializationError" /> class.
        /// </summary>
        /// <param name="toType">Type of object attempted creating from JSON.</param>
        internal JsonDeserializationError(Type toType)
            : base(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.JsonDeserializationError, toType.FullName))
        {
        }
    }

    /// <summary>
    ///     Represents an error that occurs while serializing JSON.
    /// </summary>
    public class JsonSerializationError : JsonError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonSerializationError" /> class.
        /// </summary>
        /// <param name="fromType">Type of object attempted serializing to JSON.</param>
        internal JsonSerializationError(Type fromType)
            : base(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.JsonSerializationError, fromType.FullName))
        {
        }
    }

    /// <summary>
    ///     Represents an error that occurs while deserializing JSON with no data.
    /// </summary>
    public class JsonNoDataError : JsonError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonNoDataError" /> class.
        /// </summary>
        internal JsonNoDataError()
            : base(Constants.ErrorMessages.JsonNoDataToDeserialize)
        {
        }
    }
}
