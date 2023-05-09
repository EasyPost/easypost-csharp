using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.BetaFeatures.Parameters
{
    /// <summary>
    ///     Base class for all parameters used in functions.
    /// </summary>
    public abstract class BaseParameters
    {
        /*
         * NOTES:
         * Per https://www.informit.com/articles/article.aspx?p=1997935&seqNum=5 and https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values,
         * Any nullable object (non-primitive) will default to `null`
         * Any nullable primitive will default to `null`
         * No need to set a default value for Optional parameters, will be `null` if not set, which is what the internal validator expects
         */

        /// <summary>
        ///     The internal dictionary of parameter key-value pairs.
        /// </summary>
        private Dictionary<string, object?> _parameterDictionary;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseParameters"/> class for a new set of request parameters.
        /// </summary>
        protected BaseParameters() => _parameterDictionary = new Dictionary<string, object?>();

        /// <summary>
        ///     Convert this parameter object to a dictionary for an HTTP request.
        /// </summary>
        /// <returns><see cref="Dictionary{String,TValue}" /> of parameters.</returns>
        internal virtual Dictionary<string, object> ToDictionary()
        {
            // NOTE: This method is marked internally on purpose.
            // Bad stuff could happen if we allow end-users to convert a parameter object to a dictionary themselves and try to use it in the normal functions
            // In particular, a lot of the normal functions do additional wrapping of their dictionaries, which would result in invalid JSON schemas being sent to the API

            // Construct the dictionary of all parameters
            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Instance |
                                                                BindingFlags.NonPublic |
                                                                BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                TopLevelRequestParameterAttribute? parameterAttribute = BaseCustomAttribute.GetAttribute<TopLevelRequestParameterAttribute>(property);

                // Ignore any properties that are not annotated with a StandaloneRequestParameterAttribute
                if (parameterAttribute == null)
                {
                    continue;
                }

                object? value = property.GetValue(this);

                // If the value is null, check the necessity of the parameter
                if (value == null)
                {
                    // If the parameter is required and null, throw an exception
                    if (parameterAttribute.Necessity == Necessity.Required)
                    {
                        throw new MissingParameterError(property);
                    }

                    // If the parameter is optional and null, skip it
                    continue;
                }

                // Add the non-null value to the dictionary
                Add(parameterAttribute, value);
            }

            // Return the dictionary, removing any null values now that we've verified all required parameters are set
            // Anything still null at this point is an optional parameter that was not set that can be stripped from the request
            return _parameterDictionary.ToStringNonNullableObjectDictionary();
        }

        /// <summary>
        ///     Convert this parameters object to a sub-dictionary, for use in a parent parameter object's dictionary.
        /// </summary>
        /// <param name="parentParameterObjectType">
        ///     The type of the parent parameter object in which this dictionary will be
        ///     embedded.
        /// </param>
        /// <returns><see cref="Dictionary{TKey,TValue}" /> of parameters.</returns>
        protected virtual Dictionary<string, object> ToSubDictionary(Type parentParameterObjectType)
        {
            // Construct the dictionary of all parameters
            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Instance |
                                                                BindingFlags.NonPublic |
                                                                BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                NestedRequestParameterAttribute? parameterAttribute = NestedRequestParameterAttribute.GetNestedRequestParameterAttributeForParentType(parentParameterObjectType, property);

                // Ignore any properties that are not annotated with a NestedRequestParameterAttribute or do not have a NestedRequestParameterAttribute for this specific parent type
                if (parameterAttribute == null)
                {
                    continue;
                }

                object? value = property.GetValue(this);

                // If the value is null, check the necessity of the parameter
                if (value == null)
                {
                    // If the parameter is required and null, throw an exception
                    if (parameterAttribute.Necessity == Necessity.Required)
                    {
                        throw new MissingParameterError(property);
                    }

                    // If the parameter is optional and null, skip it
                    continue;
                }

                // Add the non-null value to the dictionary
                Add(parameterAttribute, value);
            }

            // Return the dictionary, removing any null values now that we've verified all required parameters are set
            // Anything still null at this point is an optional parameter that was not set that can be stripped from the request
            return _parameterDictionary.ToStringNonNullableObjectDictionary();
        }

        /// <summary>
        ///     Add a parameter to the dictionary.
        /// </summary>
        /// <param name="requestParameterAttribute"><see cref="RequestParameterAttribute" /> of the parameter to add.</param>
        /// <param name="value">The value of parameter.</param>
        private void Add(RequestParameterAttribute requestParameterAttribute, object? value)
        {
            // Primitive types (i.e. strings, booleans) can be added directly to the dictionary

            if (!Objects.IsPrimitive(value))
            {
                value = SerializeObject(value);
            }

            _parameterDictionary = UpdateDictionary(_parameterDictionary, requestParameterAttribute.JsonPath, value);
        }

        private object? SerializeObject(object? obj)
        {
            // If the value is an object type, we know by this point it must inherit the corresponding base-IParameter interface
            // If we've done our job correctly, the only classes that inherit base-IParameter interfaces are base-EasyPostObject and base-Parameters

            switch (obj)
            {
                // If a given value is a base-EasyPostObject object, serialize it as a dictionary
                case EasyPostObject easyPostObject:
                    return easyPostObject.AsDictionary();
                // If the given value is another base-Parameters object, serialize it as a sub-dictionary for the parent dictionary
                // This is because the JSON schema for a sub-object is different than the JSON schema for a top-level object
                // e.g. the schema for an address in the address create API call is different than the schema for an address in the shipment create API call
                case BaseParameters parameters:
                    return parameters.ToSubDictionary(GetType());
                // If the given value is a list, serialize each element of the list
                case IList list:
                    {
                        var newList = new List<object?>();
                        foreach (object? subObj in list)
                        {
                            newList.Add(SerializeObject(subObj));
                        }

                        return newList;
                    }
            }

            return obj;
        }

        /// <summary>
        ///     Update a dictionary with a new value.
        /// </summary>
        /// <param name="dictionary">Dictionary to update.</param>
        /// <param name="keys">Path of new value to add.</param>
        /// <param name="value">New value to add.</param>
        /// <returns>Updated dictionary.</returns>
        /// <exception cref="Exception">Could not add value to dictionary.</exception>
        [SuppressMessage("Style", "IDE0045:Convert to conditional expression", Justification = "Harder to read")]
        private static Dictionary<string, object?> UpdateDictionary(Dictionary<string, object?>? dictionary, string[] keys, object? value)
        {
            dictionary ??= new Dictionary<string, object?>();

            switch (keys.Length)
            {
                // Don't need to go down
                case 0:
                    return dictionary;

                // Last key left
                case 1:
                    string soloKey = keys[0];
                    if (dictionary.ContainsKey(soloKey))
                    {
                        // Key-value pair already exists in dictionary (likely because of override parameters)
                        // Only change the value if the existing value is null
                        dictionary[soloKey] ??= value;
                    }
                    else
                    {
                        dictionary.Add(soloKey, value);
                    }

                    return dictionary;

                default:
                    break;
            }

            // Need to go down another level
            // Get the key and update the list of keys
            string key = keys[0];
            keys = keys.Skip(1).ToArray();
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = UpdateDictionary(new Dictionary<string, object?>(), keys, value);
            }

            object? subDirectory = dictionary[key];
            if (subDirectory is Dictionary<string, object?> subDictionary)
            {
                dictionary[key] = UpdateDictionary(subDictionary, keys, value);
            }
            else
            {
#pragma warning disable CA2201 // Don't throw base Exception class
                throw new Exception("Found a non-dictionary while traversing the dictionary");
#pragma warning restore CA2201 // Don't throw base Exception class
            }

            return dictionary;
        }
    }
}
