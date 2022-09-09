// ReSharper disable once CheckNamespace
using System.Net;
using RestSharp;

namespace EasyPost.Exceptions
{
    public class SslError : HttpError
    {
        protected SslError(int statusCode, string? errorMessage = null) : base(statusCode, errorMessage)
        {
        }
    }
}
