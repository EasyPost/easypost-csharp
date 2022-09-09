// ReSharper disable once CheckNamespace
using System.Collections.Generic;
using EasyPost.Models.API;

namespace EasyPost.Exceptions
{
    public class PaymentError : ApiError
    {
        protected PaymentError(int statusCode, string? errorMessage = null, string? code = null, List<Error>? errors = null) : base(statusCode, errorMessage, code, errors)
        {
        }
    }
}
