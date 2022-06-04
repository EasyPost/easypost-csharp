using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EasyPost.Models.V2;

namespace EasyPost.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public readonly string Code;

        public readonly List<Error> Errors;

        public readonly int StatusCode;

        /// <summary>
        ///     Constructor for an ApiException.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <param name="code">Error code.</param>
        /// <param name="message">Error message.</param>
        /// <param name="errors">A list of EasyPost.Error instances.</param>
        public ApiException(int statusCode, string code, string? message, List<Error> errors) : base(message)
        {
            StatusCode = statusCode;
            Code = code;
            Errors = errors;
        }

        /// <summary>
        ///     Load the exception data into a SerializationInfo object instance.
        /// </summary>
        /// <param name="info">SerializationInfo object instance to load data into.</param>
        /// <param name="context">StreamingContext to use for base GetObjectData call.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("StatusCode", StatusCode);
            info.AddValue("Code", Code);
            info.AddValue("Errors", Errors);
        }
    }

    [Serializable]
    public class ResourceAlreadyCreated : Exception
    {
    }

    [Serializable]
    public class PropertyMissing : Exception
    {
        private readonly string _property;

        public PropertyMissing(string property)
        {
            _property = property;
        }

        public override string Message
        {
            get { return $"Missing {_property}"; }
        }
    }

    [Serializable]
    public class ClientNotConfigured : Exception
    {
        public ClientNotConfigured(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class FilterFailure : Exception
    {
        public FilterFailure(string message) : base(message)
        {
        }
    }
}
