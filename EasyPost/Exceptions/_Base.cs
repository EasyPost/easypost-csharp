using System;

namespace EasyPost.Exceptions
{
    public class EasyPostError : Exception
    {
        internal EasyPostError(string message) : base(message)
        {
        }
    }
}
