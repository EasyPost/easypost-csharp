using System;
using EasyPost.Utilities;

namespace EasyPost.Parameters
{
    internal enum Necessity
    {
        Required,
        Optional
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal class ParameterAttribute : BaseCustomAttribute
    {
        internal string[] JsonPath { get; set; }

        internal Necessity Necessity { get; set; }

        internal ParameterAttribute(Necessity necessity, params string[] jsonPath)
        {
            Necessity = necessity;
            JsonPath = jsonPath;
        }
    }
}
