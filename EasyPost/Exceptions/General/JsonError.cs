using System;

namespace EasyPost.Exceptions.General
{
    public class JsonError : EasyPostError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonError" /> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        internal JsonError(string message) : base(message)
        {
        }
    }

    public class JsonDeserializationError : JsonError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonDeserializationError" /> class.
        /// </summary>
        /// <param name="toType">Type of object attempted creating from JSON.</param>
        internal JsonDeserializationError(Type toType) : base(string.Format(Constants.ErrorMessages.JsonDeserializationError, toType.FullName))
        {
        }
    }

    public class JsonSerializationError : JsonError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonSerializationError" /> class.
        /// </summary>
        /// <param name="fromType">Type of object attempted serializing to JSON.</param>
        internal JsonSerializationError(Type fromType) : base(string.Format(Constants.ErrorMessages.JsonSerializationError, fromType.FullName))
        {
        }
    }

    public class JsonNoDataError : JsonError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonNoDataError" /> class.
        /// </summary>
        internal JsonNoDataError() : base(Constants.ErrorMessages.JsonNoDataToDeserialize)
        {
        }
    }
}
