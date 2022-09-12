using System.Collections.Generic;
using EasyPost.Models.API;
using EasyPost.Utilities;
using RestSharp;

// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class ApiError : HttpError
    {
        public string? Code;

        public List<Error>? Errors;

        // All constructors for API exceptions are protected, so you cannot directly initialize an instance of the exception class.
        // Instead, you must use the .FromResponse method to retrieve an instance.

        protected ApiError(int statusCode, string? errorMessage = null, string? code = null, List<Error>? errors = null) : base(statusCode, errorMessage)
        {
            Code = code;
            Errors = errors;
        }

        public static new ApiError DetermineError(RestResponse response)
        {
            // TODO: Logic here to determine which type of API error to return
            return FromResponse<ApiError>(response);
        }

        public static new T FromResponse<T>(RestResponse response) where T : ApiError
        {
            int statusCode = (int)response.StatusCode;

            string? errorMessage = null;
            string? code = null;
            List<Error>? errors = null;

            try
            {
                Dictionary<string, Dictionary<string, object>> body = JsonSerialization.ConvertJsonToObject<Dictionary<string, Dictionary<string, object>>>(response.Content);

                errorMessage = body["error"]["message"].ToString();
                code = body["error"]["code"].ToString();
                errors = JsonSerialization.ConvertJsonToObject<List<Error>>(response.Content, null, new List<string>
                {
                    "error",
                    "errors"
                });
            }
            catch
            {
                errorMessage = response.Content;
                code = "RESPONSE.PARSE_ERROR";
                errors = new List<Error>();
            }

            return new ApiError(statusCode, errorMessage, code, errors) as T;
        }
    }
}
