using System;

namespace EasyPost.Utilities.Internal.Attributes
{
    /// <summary>
    ///     Custom <see cref="Attribute"/> used to label CRUD operations in this SDK.
    /// </summary>
    internal static class CrudOperations
    {
        /// <summary>
        ///     A <see cref="BaseCustomAttribute"/> used to label "create" operations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Create : BaseCustomAttribute
        {
        }

        /// <summary>
        ///     A <see cref="BaseCustomAttribute"/> used to label "read" operations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Read : BaseCustomAttribute
        {
        }

        /// <summary>
        ///     A <see cref="BaseCustomAttribute"/> used to label "update" operations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Update : BaseCustomAttribute
        {
        }

        /// <summary>
        ///     A <see cref="BaseCustomAttribute"/> used to label "delete" operations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Delete : BaseCustomAttribute
        {
        }
    }
}
