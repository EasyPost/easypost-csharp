using System;

namespace EasyPost.Utilities.Annotations
{
    internal static class CrudOperations
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        internal class Create : BaseCustomAttribute
        {
            public Create(string value)
            {
            }
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        internal class Read : BaseCustomAttribute
        {
            public Read(string value)
            {
            }
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        internal class Update : BaseCustomAttribute
        {
            public Update(string value)
            {
            }
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        internal class Delete : BaseCustomAttribute
        {
            public Delete(string value)
            {
            }
        }
    }
}
