using Xunit.Sdk;

namespace EasyPost.Tests._Utilities.Assertions
{
    /// <summary>
    /// Exception thrown when a None assertion has one or more items fail an assertion.
    /// </summary>
    public class NoneException : XunitException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NoneException"/> class.
        /// </summary>
        public NoneException()
            : base("Assert.None() Failure")
        {
        }
    }
}
