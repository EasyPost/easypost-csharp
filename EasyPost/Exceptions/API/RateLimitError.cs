﻿using System.Collections.Generic;
using EasyPost.Models.API;

// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class RateLimitError : ApiError
    {
        protected RateLimitError(int statusCode, string? errorMessage = null, string? code = null, List<Error>? errors = null) : base(statusCode, errorMessage, code, errors)
        {
        }
    }
}
