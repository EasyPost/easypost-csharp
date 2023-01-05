using System;

namespace EasyPost.Exceptions
{
#pragma warning disable SA1649
    public class EasyPostError : Exception
#pragma warning restore SA1649
    {
        internal EasyPostError(string message)
            : base(message)
        {
        }
    }
}
