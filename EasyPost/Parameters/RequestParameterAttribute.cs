using System;
using EasyPost.Utilities;

namespace EasyPost.Parameters
{
    internal enum Necessity
    {
        /**
         * * @brief
         * Required parameters are required for a request. They do not need a default value, since they are required to be set.
         * Optional parameters are optional for a request. Default value for these should be null.
        */
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
