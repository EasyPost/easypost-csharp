using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using EasyPost.Exceptions.General;
using EasyPost.Utilities.Internal.Annotations;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.BetaFeatures.Parameters
{
    /// <summary>
    ///     Base class for all parameters used in functions.
    /// </summary>
    public abstract class Parameters
    {
        /*
         * NOTES:
         * Per https://www.informit.com/articles/article.aspx?p=1997935&seqNum=5 and https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values,
         * Any nullable object (non-primitive) will default to `null`
         * Any nullable primitive will default to `null`
         * No need to set a default value for Optional parameters, will be `null` if not set, which is what the internal validator expects
         */

        private Dictionary<string, object?> _parameterDictionary;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Parameters"/> class for a new set of request parameters.
        /// </summary>
        protected Parameters() => _parameterDictionary = new Dictionary<string, object?>();

        /// <summary>
        ///     Check that all required parameters are set.
        /// </summary>
        /// <exception cref="MissingParameterError">If a required parameter is missing.</exception>
        public void Validate()
        {
            // get all public and non-public instance (non-static) properties
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            // Check that all required parameters are set
            foreach (PropertyInfo property in properties)
            {
                // Get the parameter attribute for this property
                RequestParameterAttribute? parameterAttribute = BaseCustomAttribute.GetAttribute<RequestParameterAttribute>(property);

                // If the property is not a parameter, ignore it
                if (parameterAttribute == null)
                {
                    continue;
                }

                // If the parameter is not required, skip checking the value
                if (parameterAttribute.Necessity != Necessity.Required)
                {
                    continue;
                }

                // parameter is required, check that it is set
                object? value = property.GetValue(this);

                // throw an exception if the required value is null
                if (value == null)
                {
                    throw new MissingParameterError(property);
                }

                // at this point, value is required and set, so we can continue to the next property
            }
        }

        /// <summary>
        ///     Convert the parameters to a dictionary for an HTTP request.
        /// </summary>
        /// <returns><see cref="Dictionary{String,TValue}"/> of parameters.</returns>
        public virtual Dictionary<string, object> ToDictionary()
        {
            // Construct the dictionary of all parameters
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                RequestParameterAttribute? parameterAttribute = BaseCustomAttribute.GetAttribute<RequestParameterAttribute>(property);

                // Ignore any properties that are not annotated with a RequestParameterAttribute
                if (parameterAttribute == null)
                {
                    continue;
                }

                object? value = property.GetValue(this);

                // Ignore any optional parameters that are null
                if (value == null && parameterAttribute.Necessity == Necessity.Optional)
                {
                    continue;
                }

                // Add the parameter to the dictionary
                Add(parameterAttribute, value);
            }

            // Verify that all required parameters are set in the dictionary
            Validate();

            // Return the dictionary, removing any null values now that we've verified all required parameters are set
            // Anything still null at this point is an optional parameter that was not set that can be stripped from the request
            return _parameterDictionary.ToStringNonNullableObjectDictionary();
        }

        /// <summary>
        ///     Add a parameter to the dictionary.
        /// </summary>
        /// <param name="requestParameterAttribute"><see cref="RequestParameterAttribute"/> of the parameter to add.</param>
        /// <param name="value">The value of parameter.</param>
        private void Add(RequestParameterAttribute requestParameterAttribute, object? value)
        {
            // If a given property is an EasyPostObject, the JsonProperty attributes would normally serialize the object as a dictionary later, during RestRequest
            // TODO: We could explicitly convert an EasyPostObject into just an ID here

            // If the given property is another Parameters object, we'll need to convert it to a dictionary first
            if (value is Parameters parameters)
            {
                value = parameters?.ToDictionary();
            }

            // Otherwise, simply add the primitive value to the dictionary.
            _parameterDictionary = UpdateDictionary(this._parameterDictionary, requestParameterAttribute.JsonPath, value);
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

        /// <summary>
        ///     Check that a value exists in the dictionary at a given path.
        /// </summary>
        /// <param name="dictionary">Dictionary to check.</param>
        /// <param name="keys">Path to look for.</param>
        /// <returns>True if value exists, false otherwise.</returns>
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

            return value switch
            {
                null => false, // value of the key is null
                Dictionary<string, object?> nestedDictionary when keys.Count > 1 => ValueExistsInDictionary(nestedDictionary, keys.Skip(1).ToList()), // we need to go down another level and check this nested dictionary, only because we have more keys to check
                var _ => true, // If we haven't failed yet, then the key exists, the value is not null and we have no more keys to check
            };
        }
    }
}
