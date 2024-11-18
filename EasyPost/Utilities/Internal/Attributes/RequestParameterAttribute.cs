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

    /// <summary>
    ///     An enum to represent the state of an independent parameter.
    /// </summary>
    public enum IndependentStatus
    {
        /// <summary>
        ///     Denote the condition when an independent parameter is set.
        /// </summary>
        IfSet,

        /// <summary>
        ///     Denote the condition when an independent parameter is not set.
        /// </summary>
        IfNotSet,

        /// <summary>
        ///     Denote the condition when an independent parameter is a specific value.
        /// </summary>
        IfValue,

        /// <summary>
        ///     Denote the condition when an independent parameter is not a specific value.
        /// </summary>
        IfNotValue,
    }

    /// <summary>
    ///     An enum to represent the state of a dependent parameter.
    /// </summary>
    public enum DependentStatus
    {
        /// <summary>
        ///     Denote that a dependent parameter must be set.
        /// </summary>
        MustBeSet,

        /// <summary>
        ///     Denote that a dependent parameter must not be set.
        /// </summary>
        MustNotBeSet,

        /// <summary>
        ///     Denote that a dependent parameter must be a specific value.
        /// </summary>
        MustBeValue,

        /// <summary>
        ///     Denote that a dependent parameter must not be a specific value.
        /// </summary>
        MustNotBeValue,
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
    ///     A <see cref="BaseCustomAttribute"/> to denote the required dependents of a request parameter, and the conditions under which they are required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public abstract class RequestParameterDependentsAttribute : BaseCustomAttribute
    {
        /// <summary>
        ///     The conditional set status of the independent property.
        /// </summary>
        private IndependentStatus IndependentStatus { get; }

        /// <summary>
        ///     The evaluated value of the independent property.
        /// </summary>
        private object? IndependentValue { get; }

        /// <summary>
        ///     The expected set status of the dependent properties.
        /// </summary>
        private DependentStatus DependentStatus { get; }

        /// <summary>
        ///     The expected value of the dependent properties.
        /// </summary>
        private object? DependentValue { get; }

        /// <summary>
        ///     The names of the dependent properties.
        /// </summary>
        private List<string> DependentProperties { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        protected RequestParameterDependentsAttribute(IndependentStatus independentStatus, DependentStatus dependentStatus, params string[] dependentProperties)
        {
            IndependentStatus = independentStatus;
            DependentStatus = dependentStatus;
            DependentProperties = dependentProperties.ToList();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="independentValue">The value of the independent property. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentValue">The value of the dependent properties. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        protected RequestParameterDependentsAttribute(IndependentStatus independentStatus, object independentValue, DependentStatus dependentStatus, object dependentValue, params string[] dependentProperties)
        {
            IndependentStatus = independentStatus;
            IndependentValue = independentValue;
            DependentStatus = dependentStatus;
            DependentValue = dependentValue;
            DependentProperties = dependentProperties.ToList();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentValue">The value of the dependent properties. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        protected RequestParameterDependentsAttribute(IndependentStatus independentStatus, DependentStatus dependentStatus, object dependentValue, params string[] dependentProperties)
        {
            IndependentStatus = independentStatus;
            DependentStatus = dependentStatus;
            DependentValue = dependentValue;
            DependentProperties = dependentProperties.ToList();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="independentValue">The value of the independent property. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        protected RequestParameterDependentsAttribute(IndependentStatus independentStatus, object independentValue, DependentStatus dependentStatus, params string[] dependentProperties)
        {
            IndependentStatus = independentStatus;
            IndependentValue = independentValue;
            DependentStatus = dependentStatus;
            DependentProperties = dependentProperties.ToList();
        }

        /// <summary>
        ///     Check that the expected value state of the property is met.
        /// </summary>
        /// <param name="propertyValue">Optional, the value of the independent property.</param>
        /// <param name="dependentPropertyValue">The value of the dependent property. Do not pass in <see cref="ValueEnum"/>; instead, pass in the underlying value.</param>
        /// <returns>True if the dependent property meets the dependency condition, false otherwise.</returns>
        private bool DependencyConditionPasses(object? propertyValue, object? dependentPropertyValue)
        {
            switch (IndependentStatus)
            {
                // Skip value check if the independent condition is not present
                case IndependentStatus.IfValue when propertyValue == null: // Independent value is not set and needs to be to proceed with comparison
                case IndependentStatus.IfValue when !propertyValue.Equals(IndependentValue): // Independent value does not match and needs to
                case IndependentStatus.IfNotValue when propertyValue == null: // Independent value is not set and needs to be to proceed with comparison
                case IndependentStatus.IfNotValue when propertyValue.Equals(IndependentValue): // Independent value matches and does not need to
                case IndependentStatus.IfSet when propertyValue == null: // Independent value is not set and needs to be
                case IndependentStatus.IfNotSet when propertyValue != null: // Independent value is set and needs to not be
                    return true;
                // Independent condition is present, now check dependent condition
                default:
                    return DependentStatus switch
                    {
                        DependentStatus.MustBeSet => dependentPropertyValue != null,
                        DependentStatus.MustNotBeSet => dependentPropertyValue == null,
                        DependentStatus.MustBeValue => dependentPropertyValue != null && dependentPropertyValue.Equals(DependentValue),
                        DependentStatus.MustNotBeValue => dependentPropertyValue == null || !dependentPropertyValue.Equals(DependentValue),
                        var _ => false,
                    };
            }
        }

        /// <summary>
        ///     Check that all dependent properties are compliant with the dependency conditions.
        /// </summary>
        /// <param name="obj">The object containing the dependent properties.</param>
        /// <param name="propertyValue">The value of the independent property. A <see cref="ValueEnum"/> will be converted to its underlying value.</param>
        /// <returns>A tuple containing a boolean indicating whether the dependency is met, and a string containing the name of the first dependent property that does not meet the dependency conditions.</returns>
        public Tuple<bool, string> DependentsAreCompliant(object obj, object? propertyValue)
        {
            // Convert any value enums to their underlying values (this cannot work with non-value Enums, but those can't be passed to attributes, so it is safe to ignore)
            if (propertyValue != null && Objects.IsValueEnum(propertyValue))
            {
                ValueEnum enumValue = (ValueEnum)propertyValue;
                propertyValue = enumValue.Value;
            }

            // No need to check dependent IfSet properties if the property is not set
            if (propertyValue == null && IndependentStatus == IndependentStatus.IfSet)
            {
                return new Tuple<bool, string>(true, string.Empty);
            }

            // No need to check dependent IfNotSet properties if the property is set
            if (propertyValue != null && IndependentStatus == IndependentStatus.IfNotSet)
            {
                return new Tuple<bool, string>(true, string.Empty);
            }

            foreach (string dependentPropertyName in DependentProperties)
            {
                PropertyInfo? dependentProperty = obj.GetType().GetProperty(dependentPropertyName);

                // If a listed dependent property is not found, the dependency is not met (and there is likely a bug in the parameter set source code)
                if (dependentProperty == null)
                {
                    return new Tuple<bool, string>(false, dependentPropertyName);
                }

                object? dependentPropertyValue = dependentProperty.GetValue(obj);
                // If the dependent property does not meet the dependency condition, the dependency is not met
                if (!DependencyConditionPasses(propertyValue, dependentPropertyValue))
                {
                    return new Tuple<bool, string>(false, dependentPropertyName);
                }
            }

            // If all dependent properties meet the dependency conditions, the dependency is met
            return new Tuple<bool, string>(true, string.Empty);
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
    ///     A <see cref="BaseCustomAttribute"/> to denote the required dependents of a top-level request parameter, and the conditions under which they are required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class TopLevelRequestParameterDependentsAttribute : RequestParameterDependentsAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TopLevelRequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        public TopLevelRequestParameterDependentsAttribute(IndependentStatus independentStatus, DependentStatus dependentStatus, params string[] dependentProperties)
            : base(independentStatus, dependentStatus, dependentProperties)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TopLevelRequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="independentValue">The value of the independent property. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentValue">The value of the dependent properties. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        public TopLevelRequestParameterDependentsAttribute(IndependentStatus independentStatus, object independentValue, DependentStatus dependentStatus, object dependentValue, params string[] dependentProperties)
            : base(independentStatus, independentValue, dependentStatus, dependentValue, dependentProperties)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TopLevelRequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentValue">The value of the dependent properties. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        public TopLevelRequestParameterDependentsAttribute(IndependentStatus independentStatus, DependentStatus dependentStatus, object dependentValue, params string[] dependentProperties)
            : base(independentStatus, dependentStatus, dependentValue, dependentProperties)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TopLevelRequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="independentValue">The value of the independent property. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        public TopLevelRequestParameterDependentsAttribute(IndependentStatus independentStatus, object independentValue, DependentStatus dependentStatus, params string[] dependentProperties)
            : base(independentStatus, independentValue, dependentStatus, dependentProperties)
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

    /// <summary>
    ///     A <see cref="BaseCustomAttribute"/> to denote the required dependents of a nested request parameter, and the conditions under which they are required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class NestedRequestParameterDependentsAttribute : RequestParameterDependentsAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NestedRequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        public NestedRequestParameterDependentsAttribute(IndependentStatus independentStatus, DependentStatus dependentStatus, params string[] dependentProperties)
            : base(independentStatus, dependentStatus, dependentProperties)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NestedRequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="independentValue">The value of the independent property. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentValue">The value of the dependent properties. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        public NestedRequestParameterDependentsAttribute(IndependentStatus independentStatus, object independentValue, DependentStatus dependentStatus, object dependentValue, params string[] dependentProperties)
            : base(independentStatus, independentValue, dependentStatus, dependentValue, dependentProperties)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NestedRequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentValue">The value of the dependent properties. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        public NestedRequestParameterDependentsAttribute(IndependentStatus independentStatus, DependentStatus dependentStatus, object dependentValue, params string[] dependentProperties)
            : base(independentStatus, dependentStatus, dependentValue, dependentProperties)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NestedRequestParameterDependentsAttribute"/> class.
        /// </summary>
        /// <param name="independentStatus">The set status of the independent property.</param>
        /// <param name="independentValue">The value of the independent property. If enforcing a custom <see cref="ValueEnum"/>, provide the underlying value.</param>
        /// <param name="dependentStatus">The set status of the dependent properties.</param>
        /// <param name="dependentProperties">The names of the dependent properties.</param>
        public NestedRequestParameterDependentsAttribute(IndependentStatus independentStatus, object independentValue, DependentStatus dependentStatus, params string[] dependentProperties)
            : base(independentStatus, independentValue, dependentStatus, dependentProperties)
        {
        }
    }

#pragma warning restore CA1019 // Define accessors for attribute arguments
}
