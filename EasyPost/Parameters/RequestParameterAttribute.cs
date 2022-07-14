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
    internal class RequestParameterAttribute : BaseCustomAttribute
    {
        internal string[] JsonPath { get; }

        internal Necessity Necessity { get; }

        internal RequestParameterAttribute(Necessity necessity, params string[] jsonPath)
        {
            Necessity = necessity;
            JsonPath = jsonPath;
        }
    }
}
