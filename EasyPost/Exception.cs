// Exception.cs
// See LICENSE for licensing info.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace EasyPost
{
    [Serializable]
    public class HttpException : Exception
    {
        public string Code;

        public List<Error> Errors;

        public int StatusCode;

        /// <summary>
        ///     Constructor for an HttpException.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <param name="code">Error code.</param>
        /// <param name="message">Error message.</param>
        /// <param name="errors">A list of EasyPost.Error instances.</param>
        public HttpException(int statusCode, string code, string message, List<Error> errors) : base(message)
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
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
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
    public class ClientNotConfigured : Exception
    {
    }
}
