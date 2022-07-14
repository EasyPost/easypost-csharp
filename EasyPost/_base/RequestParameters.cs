using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyPost.ApiCompatibility;
using EasyPost.Exceptions;
using EasyPost.Parameters;
using EasyPost.Utilities;

namespace EasyPost._base
{
    /// <summary>
    ///     Class for parameters for EasyPost API calls.
    /// </summary>
    public abstract class RequestParameters : IRequestParameters
    {
        private Dictionary<string, object?>? _parameterDictionary = new Dictionary<string, object?>();

        protected RequestParameters(Dictionary<string, object?>? overrideParameters = null)
        {
            if (overrideParameters != null)
            {
                _parameterDictionary = overrideParameters;
            }
        }

        internal abstract Dictionary<string, object?>? ToDictionary(EasyPostClient client);

        protected static Dictionary<string, object?>? ToDictionary(RequestParameters obj, EasyPostClient client)
        {
            // Construct the dictionary of all parameters for this API version
            RegisterParameters(obj, client);
            // Verify that all required parameters are set in the dictionary
            VerifyParameters(obj);
            return obj._parameterDictionary;
        }

        private static void Add(RequestParameterAttribute requestParameterAttribute, RequestParameters obj, object? value)
        {
            // If a given property is an EasyPostObject, the JsonProperty attributes will serialize the object as a dictionary (later, during RestRequest)
            // Otherwise, simply add the primitive value to the dictionary.
            obj._parameterDictionary = UpdateDictionary(obj._parameterDictionary, requestParameterAttribute.JsonPath, value);
        }

        /// <summary>
        ///     Build a dictionary from the parameters.
        /// </summary>
        /// <param name="obj">Parameters collection to construct a dictionary from.</param>
        /// <param name="client">EasyPost client being used for the request these parameters are for.</param>
        private static void RegisterParameters(RequestParameters obj, EasyPostClient client)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                RequestParameterAttribute? parameterAttribute = BaseCustomAttribute.GetCustomAttribute<RequestParameterAttribute>(property);
                if (parameterAttribute == null)
                {
                    // Ignore any properties that are not annotated with a ParameterAttribute
                    continue;
                }

                if (!ApiCompatibilityAttribute.CheckParameterCompatible(property.Name, obj.GetType(), client))
                {
                    // Ignore any parameters that are not compatible with this API version
                    continue;
                }

                object? value = property.GetValue(obj);
                if (value == null && parameterAttribute.Necessity == Necessity.Optional)
                {
                    // Ignore any optional parameters that are null
                    continue;
                }

                Add(parameterAttribute, obj, value);
            }
        }

        private static Dictionary<string, object?> UpdateDictionary(Dictionary<string, object?>? dictionary, string[] keys, object? value)
        {
            // TODO: Check this line if issues arise.
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
                        dictionary[soloKey] ??= value;
                    }
                    else
                    {
                        dictionary.Add(soloKey, value);
                    }

                    return dictionary;
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
                throw new Exception("Found a non-dictionary while traversing the dictionary");
            }

            return dictionary;
        }

        /// <summary>
        ///     Check that a value
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        private static bool ValueExistsInDictionary(IReadOnlyDictionary<string, object?>? dictionary, IReadOnlyList<string> keys)
        {
            if (keys.Count == 0)
            {
                return true; // no keys to check (this is the end of recursion), so return true
            }

            if (dictionary == null)
            {
                return false; // no dictionary, can't be in the dictionary
            }

            string key = keys[0];

            if (!dictionary.ContainsKey(key))
            {
                return false; // key does not exist in the dictionary
            }

            bool valueRetrieved = dictionary.TryGetValue(key, out object? value);

            if (!valueRetrieved)
            {
                return false; // could not retrieve value of the key
            }

            if (value == null)
            {
                return false; // value of the key is null
            }

            if (value is Dictionary<string, object?> nestedDictionary && keys.Count > 1)
            {
                // we need to go down another level and check this nested dictionary, only because we have more keys to check
                return ValueExistsInDictionary(nestedDictionary, keys.Skip(1).ToList());
            }

            // If we haven't failed yet, then the key exists, the value is not null and we have no more keys to check
            return true;
        }

        /// <summary>
        ///     Check that all required parameters are set.
        /// </summary>
        /// <param name="obj">Parameter collection to verify.</param>
        /// <exception cref="MissingRequiredParameterException">If a required parameter is missing.</exception>
        private static void VerifyParameters(RequestParameters obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                RequestParameterAttribute? parameterAttribute = BaseCustomAttribute.GetCustomAttribute<RequestParameterAttribute>(property);
                if (parameterAttribute == null)
                {
                    continue;
                }

                if (parameterAttribute.Necessity == Necessity.Required && !ValueExistsInDictionary(obj._parameterDictionary, parameterAttribute.JsonPath))
                {
                    throw new MissingRequiredParameterException(property);
                }
            }
        }
    }

    internal interface IRequestParameters
    {
    }
}
