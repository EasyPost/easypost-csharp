﻿using System.Collections.Generic;
using EasyPost.Models.API;

namespace EasyPost.Exceptions.API
{
    /// <summary>
    ///     Represents an error that occurs when a payment error occurs on the EasyPost API.
    /// </summary>
    public class PaymentError : ApiError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PaymentError" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message to print to console.</param>
        /// <param name="statusCode">Optional HTTP status code to store as a property.</param>
        /// <param name="errorType">Optional error type string to store as a property.</param>
        /// <param name="errors">Optional list of <see cref="Error"/> objects to store as a property.</param>
        internal PaymentError(string errorMessage, int statusCode, string? errorType = null, List<Error>? errors = null)
            : base(errorMessage, statusCode, errorType, errors)
        {
        }
    }
}
