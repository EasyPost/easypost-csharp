using EasyPost.Utilities.Internal.Attributes;

// ReSharper disable InconsistentNaming

namespace EasyPost.Integration.Utilities.Attributes
{
    internal static class Testing
    {
        /// <summary>
        ///     Marks an integration test that is testing access levels (test that users can/cannot access a function or property as expected)
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Access : BaseCustomAttribute;

        /// <summary>
        ///     Marks an integration test that is testing compile-time behavior (test that code as written will compile)
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Compile : BaseCustomAttribute;

        /// <summary>
        ///     Marks an integration test that is testing run-time behavior (test that code as written will run)
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Run : BaseCustomAttribute;
    }
}
