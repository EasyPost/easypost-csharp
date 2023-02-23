using System;

namespace EasyPost.Utilities.Annotations
{
    /// <summary>
    ///     An enum to represent the necessity of a parameter.
    /// </summary>
    internal enum Necessity
    {
        /// <summary>
        ///     Required parameters are required for a request. They do not need a default value, since they are required to be set.
        /// </summary>
        Required,

        /// <summary>
        ///     Optional parameters are optional for a request. Default value for these should be null.
        /// </summary>
        Optional,
    }

    /// <summary>
    ///     An attribute to label a parameter that will be sent in an HTTP request to the EasyPost API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal class RequestParameterAttribute : BaseCustomAttribute
    {
        /// <summary>
        ///     Gets the <see cref="Necessity"/> of the parameter.
        /// </summary>
        internal Necessity Necessity { get; }

        /// <summary>
        ///     Gets the keys, in order, where the value of the property should be placed in the JSON data.
        /// </summary>
        internal string[] JsonPath { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RequestParameterAttribute"/> class with the given <see cref="Necessity"/> and JSON path.
        /// </summary>
        /// <param name="necessity"><see cref="Necessity"/> level of this parameter.</param>
        /// <param name="jsonPath">Path in JSON schema where this parameter value will be inserted.</param>
        internal RequestParameterAttribute(Necessity necessity, params string[] jsonPath)
        {
            Necessity = necessity;
            JsonPath = jsonPath;
        }
    }
}
