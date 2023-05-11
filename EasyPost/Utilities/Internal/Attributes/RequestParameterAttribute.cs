using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyPost.Utilities.Internal.Attributes
{
    /// <summary>
    ///     An enum to represent the necessity of a parameter.
    /// </summary>
    public enum Necessity
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

#pragma warning disable CA1019 // Define accessors for attribute arguments
    /// <summary>
    ///     A <see cref="BaseCustomAttribute"/> to label a parameter that will be sent in an HTTP request to the EasyPost API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public abstract class RequestParameterAttribute : BaseCustomAttribute
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
        ///     Initializes a new instance of the <see cref="RequestParameterAttribute"/> class.
        /// </summary>
        /// <param name="necessity"><see cref="Necessity"/> level of this parameter.</param>
        /// <param name="jsonPath">Path in JSON schema where this parameter value will be inserted.</param>
        protected RequestParameterAttribute(Necessity necessity, params string[] jsonPath)
        {
            Necessity = necessity;
            JsonPath = jsonPath;
        }
    }

    /// <summary>
    ///     A <see cref="BaseCustomAttribute"/> to label a parameter that will be included in a top-level (standalone) JSON request body.
    /// </summary>
    public sealed class TopLevelRequestParameterAttribute : RequestParameterAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TopLevelRequestParameterAttribute"/> class.
        /// </summary>
        /// <param name="necessity"><see cref="Necessity"/> level of this parameter.</param>
        /// <param name="jsonPath">Path in JSON schema where this parameter value will be inserted.</param>
        public TopLevelRequestParameterAttribute(Necessity necessity, params string[] jsonPath)
            : base(necessity, jsonPath)
        {
        }
    }

    /// <summary>
    ///     A <see cref="BaseCustomAttribute"/> to label a parameter that will be included in an embedded dictionary inside another JSON request body (e.g. "address" data in "shipment" parameters).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class NestedRequestParameterAttribute : RequestParameterAttribute
    {
        /// <summary>
        ///     The type of the parent parameter set that will utilize this parameter.
        /// </summary>
        private Type ParentType { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NestedRequestParameterAttribute"/> class.
        /// </summary>
        /// <param name="parentType">The type of the parent parameter set that will utilize this parameter.</param>
        /// <param name="necessity"><see cref="Necessity"/> level of this parameter.</param>
        /// <param name="jsonPath">Path in JSON schema where this parameter value will be inserted.</param>
        public NestedRequestParameterAttribute(Type parentType, Necessity necessity, params string[] jsonPath)
            : base(necessity, jsonPath) => ParentType = parentType;

        /// <summary>
        ///     Get the proper <see cref="NestedRequestParameterAttribute"/> for the provided property, based on the provided parent type.
        /// </summary>
        /// <param name="parentType">The parent type used to determine which <see cref="NestedRequestParameterAttribute"/> to retrieve.</param>
        /// <param name="property">The property to retrieve the <see cref="NestedRequestParameterAttribute"/> for.</param>
        /// <returns>The proper <see cref="NestedRequestParameterAttribute"/>, or null if no matching attribute found.</returns>
        internal static NestedRequestParameterAttribute? GetNestedRequestParameterAttributeForParentType(Type parentType, PropertyInfo property)
        {
            IEnumerable<NestedRequestParameterAttribute> attributes = property.GetCustomAttributes<NestedRequestParameterAttribute>();
            return attributes.FirstOrDefault(attribute => attribute.ParentType == parentType);
        }
    }
#pragma warning restore CA1019 // Define accessors for attribute arguments
}
