using System;
using EasyPost.Utilities;

namespace EasyPost.Parameters
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal class ParameterAttribute : BaseCustomAttribute
    {
        internal string[] JsonPath { get; set; }

        internal ParameterAttribute(params string[] jsonPath)
        {
            JsonPath = jsonPath;
        }
    }
}
