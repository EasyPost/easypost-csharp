using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyPost.Exceptions;
using EasyPost.Parameters;
using EasyPost.Utilities;

namespace EasyPost.Interfaces
{
    public abstract class EasyPostParameters : IEasyPostParameters
    {
        private Dictionary<string, object?>? _parameterDictionary = new Dictionary<string, object?>();

        protected EasyPostParameters(Dictionary<string, object?>? overrideParameters = null)
        {
            if (overrideParameters == null)
            {
                return;
            }

            foreach (var kvp in overrideParameters)
            {
                _parameterDictionary[kvp.Key] = kvp.Value;
            }
        }

        internal abstract Dictionary<string, object?>? ToDictionary();

        protected static Dictionary<string, object?>? ToDictionary(EasyPostParameters obj)
        {
            // Construct the dictionary of all parameters
            RegisterParameters(obj);
            // Verify that all required parameters are set in the dictionary
            VerifyParameters(obj);
            return obj._parameterDictionary;
        }

        private static void Add(ParameterAttribute parameterAttribute, PropertyInfo propertyInfo, EasyPostParameters obj)
        {
            // If a given property is an EasyPostObject, the JsonProperty attributes will serialize the object as a dictionary (later, during RestRequest)
            // Otherwise, simply add the primitive value to the dictionary.
            obj._parameterDictionary = UpdateDictionary(obj._parameterDictionary, parameterAttribute.JsonPath, propertyInfo.GetValue(obj));
        }

        /// <summary>
        ///     Build a dictionary from the parameters.
        /// </summary>
        /// <param name="obj">Parameters collection to construct a dictionary from.</param>
        private static void RegisterParameters(EasyPostParameters obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                ParameterAttribute? parameterAttribute = BaseCustomAttribute.GetCustomAttribute<ParameterAttribute>(property);
                if (parameterAttribute == null)
                {
                    continue;
                }

                Add(parameterAttribute, property, obj);
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
                    // TODO: Check this line if issues arise.
                    dictionary[keys[0]] = value;
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
        private static void VerifyParameters(EasyPostParameters obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                ParameterAttribute? parameterAttribute = BaseCustomAttribute.GetCustomAttribute<ParameterAttribute>(property);
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

    internal interface IEasyPostParameters
    {
    }
}
