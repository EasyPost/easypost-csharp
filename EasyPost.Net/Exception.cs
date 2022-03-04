using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace EasyPost
{
    [Serializable]
    public class ApiException : Exception
    {
        public string? Code;

        public ApiError ApiError;

        public int StatusCode;

        /// <summary>
        ///     Constructor for an ApiException.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <param name="code">Error code.</param>
        /// <param name="message">Error message.</param>
        /// <param name="apiError">An EasyPost.ApiError instance.</param>
        public ApiException(int statusCode, string? code, string message, ApiError apiError) : base(message)
        {
            StatusCode = statusCode;
            Code = code;
            ApiError = apiError;
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
            info.AddValue("Error", ApiError);
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
