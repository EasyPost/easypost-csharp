using System;
using System.Reflection;

namespace EasyPost.Exceptions
{
    [Serializable]
    internal class MissingRequiredParameter : Exception
    {
        internal MissingRequiredParameter(PropertyInfo property) : base($"{property.Name} is a required parameter.")
        {
        }
    }
}
