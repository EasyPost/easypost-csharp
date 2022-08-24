using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EasyPost.Models.API;
using EasyPost.Utilities;
using RestSharp;

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
        public HttpException(int statusCode, string code, string? message, List<Error> errors) : base(message)
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

        public static HttpException FromResponse(RestResponse response)
        {
            int statusCode = (int)response.StatusCode;

            try
            {
                Dictionary<string, Dictionary<string, object>> body = JsonSerialization.ConvertJsonToObject<Dictionary<string, Dictionary<string, object>>>(response.Content);
                List<Error> errors = JsonSerialization.ConvertJsonToObject<List<Error>>(response.Content, null, new List<string>
                {
                    "error",
                    "errors"
                });

                return new HttpException(
                    statusCode,
                    (string)body["error"]["code"],
                    (string)body["error"]["message"],
                    errors
                );
            }
            catch
            {
                return new HttpException(statusCode, "RESPONSE.PARSE_ERROR", response.Content, new List<Error>());
            }
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

        public override string Message
        {
            get { return $"Missing {_property}"; }
        }

        public PropertyMissing(string property)
        {
            _property = property;
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
