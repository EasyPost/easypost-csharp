using System;
using Xunit.Sdk;

namespace EasyPost.Tests._Utilities.Assertions
{
    /// <summary>
    ///     Exception thrown when a DoesNotThrow assertion fails.
    /// </summary>
    public class DoesNotThrowException : XunitException
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="DoesNotThrowException"/> class.
        /// </summary>
        public DoesNotThrowException(Exception ex)
            : base("Assert.DoesNotThrow() Failure", ex)
        {
        }
    }
}
