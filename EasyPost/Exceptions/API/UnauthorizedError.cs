﻿// ReSharper disable once CheckNamespace
using System.Collections.Generic;
using EasyPost.Models.API;

namespace EasyPost.Exceptions
{
    public class UnauthorizedError : ApiError
    {
        protected UnauthorizedError(int statusCode, string? errorMessage = null, string? code = null, List<Error>? errors = null) : base(statusCode, errorMessage, code, errors)
        {
        }
    }
}
