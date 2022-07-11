using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyPost.Parameters;
using EasyPost.Utilities;

namespace EasyPost.Interfaces
{
    public abstract class EasyPostParameters : IEasyPostParameters
    {
        protected Dictionary<string, object?>? ParameterDictionary = new Dictionary<string, object?>();

        protected EasyPostParameters(Dictionary<string, object?>? overrideParameters = null)
        {
            if (overrideParameters == null)
            {
                return;
            }

            foreach (var kvp in overrideParameters)
            {
                ParameterDictionary[kvp.Key] = kvp.Value;
            }
        }

        internal abstract Dictionary<string, object?>? ToDictionary();

        protected void RegisterParameters(EasyPostParameters obj)
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

        private void Add(ParameterAttribute parameterAttribute, PropertyInfo propertyInfo, EasyPostParameters obj)
        {
            // If a given property is an EasyPostObject, the JsonProperty attributes will serialize the object as a dictionary (later, during RestRequest)
            // Otherwise, simply add the primitive value to the dictionary.
            ParameterDictionary = UpdateDictionary(ParameterDictionary, parameterAttribute.JsonPath, propertyInfo.GetValue(obj));
        }

        private Dictionary<string, object?>? UpdateDictionary(Dictionary<string, object?>? dictionary, string[] keys, object? value)
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
    }

    internal interface IEasyPostParameters
    {
    }
}
