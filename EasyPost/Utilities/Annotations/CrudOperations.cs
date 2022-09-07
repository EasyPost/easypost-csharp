using System;

namespace EasyPost.Utilities.Annotations
{
    internal static class CrudOperations
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        internal class Create : BaseCustomAttribute
        {
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        internal class Read : BaseCustomAttribute
        {
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        internal class Update : BaseCustomAttribute
        {
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        internal class Delete : BaseCustomAttribute
        {
        }
    }
}
