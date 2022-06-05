using System;
using System.Net;
using System.Runtime.Serialization;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class HttpException : Exception
    {
        public readonly int StatusCode;
        public readonly string StatusMessage;

        /// <summary>
        ///     Constructor for an HttpException.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode object.</param>
        /// <param name="message">Custom exception message.</param>
        internal HttpException(HttpStatusCode statusCode, string? message) : base(message)
        {
            StatusCode = (int)statusCode;
            StatusMessage = statusCode.ToString();
        }

        /// <summary>
        ///     Load the exception data into a SerializationInfo object instance.
        /// </summary>
        /// <param name="info">SerializationInfo object instance to load data into.</param>
        /// <param name="context">StreamingContext to use for base GetObjectData call.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("HTTP Status Code", StatusCode);
            info.AddValue("HTTP Status Message", StatusMessage);
        }
    }
}
