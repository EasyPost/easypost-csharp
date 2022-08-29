using System;

namespace EasyPost.Utilities.Annotations
{
    internal abstract class BaseCustomAttribute : Attribute, IBaseCustomAttribute
    {
    }

    internal interface IBaseCustomAttribute
    {
    }
}
