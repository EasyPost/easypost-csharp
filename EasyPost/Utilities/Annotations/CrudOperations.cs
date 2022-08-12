using System;

namespace EasyPost.Utilities.Annotations
{
    internal static class CrudOperations
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        internal class Create : BaseCustomAttribute
        {
            public Create()
            {
            }
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        internal class Read : BaseCustomAttribute
        {
            public Read()
            {
            }
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        internal class Update : BaseCustomAttribute
        {
            public Update()
            {
            }
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        internal class Delete : BaseCustomAttribute
        {
            public Delete()
            {
            }
        }
    }
}
