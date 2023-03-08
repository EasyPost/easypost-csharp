using System;

namespace EasyPost.Utilities.Internal.Attributes
{
    internal static class CrudOperations
    {
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Create : BaseCustomAttribute
        {
        }

        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Read : BaseCustomAttribute
        {
        }

        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Update : BaseCustomAttribute
        {
        }

        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Delete : BaseCustomAttribute
        {
        }
    }
}
