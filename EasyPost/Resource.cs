﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyPost.Utilities;
using Newtonsoft.Json;

namespace EasyPost
{
    public class Resource
    {
        public override bool Equals(object obj)
        {
            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            string? thisJson = AsJson();
            string? otherJson = ((Resource)obj).AsJson();
            if (thisJson == null || otherJson == null)
            {
                // can't do proper comparison if either or both could not be serialized
                return false;
            }
            return thisJson == otherJson;
        }

        /// <summary>
        ///     Get the dictionary representation of this object instance.
        /// </summary>
        /// <returns>A key-value dictionary representation of this object instance's attributes.</returns>
        [Obsolete("This method is not intended for end-user use, and will be made inaccessible in a future update.")]
        public Dictionary<string, object?> AsDictionary() =>
            GetType()
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(info => info.Name, info => GetValue(info));

        /// <summary>
        ///     Get the JSON representation of this object instance.
        /// </summary>
        /// <returns>A JSON string representation of this object instance's attributes</returns>
        [Obsolete("This method is not intended for end-user use, and will be made inaccessible in a future update.")]
        public string? AsJson()
        {
            return JsonSerialization.ConvertObjectToJson(this);
        }

        /// <summary>
        ///     Merge the properties of this object instance with the properties of another object instance.
        ///     Adds properties from the input object instance into this object instance.
        /// </summary>
        /// <param name="source">Object instance to extract properties from to merge into this object instance.</param>
        [Obsolete("This method is not intended for end-user use, and will be made inaccessible in a future update.")]
        public void Merge(object source)
        {
            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(source, null), null);
            }
        }

        private object? GetValue(PropertyInfo info)
        {
            object? value = info.GetValue(this, null);

            switch (value)
            {
                case Resource resource:
                    return resource.AsDictionary();
                case IEnumerable<Resource> enumerable:
                    List<Dictionary<string, object?>> values = new List<Dictionary<string, object?>>();
                    foreach (Resource resource in enumerable)
                    {
                        values.Add(resource.AsDictionary());
                    }
                    return values;
                default:
                    return value;
            }
        }

        /// <summary>
        ///     Deserialize a JSON string into an object instance.
        /// </summary>
        /// <param name="json">The JSON to deserialize.</param>
        /// <typeparam name="T">The type of object to generate.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        [Obsolete("This method is not intended for end-user use, and will be made inaccessible in a future update.")]
        public static T Load<T>(string json) where T : Resource => JsonSerialization.ConvertJsonToObject<T>(json);

        /// <summary>
        ///     Load a dictionary of properties into an object instance.
        /// </summary>
        /// <param name="attributes">A dictionary of key-value pairs of attributes for the object instance.</param>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        [Obsolete("This method is not intended for end-user use, and will be made inaccessible in a future update.")]
        public static T LoadFromDictionary<T>(Dictionary<string, object> attributes) where T : Resource
        {
            Type type = typeof(T);
            T resource = (T)Activator.CreateInstance(type);

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (attributes.TryGetValue(property.Name, out object? attribute) == false)
                {
                    continue;
                }

                if (property.PropertyType.GetInterfaces().Contains(typeof(Resource)))
                {
                    MethodInfo method = property.PropertyType
                        .GetMethod("LoadFromDictionary",
                            BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public)
                        .MakeGenericMethod(property.PropertyType);

                    property.SetValue(resource, method.Invoke(resource, new[]
                    {
                        attribute
                    }), null);
                }
                else if (typeof(IEnumerable<Resource>).IsAssignableFrom(property.PropertyType))
                {
                    property.SetValue(resource, Activator.CreateInstance(property.PropertyType), null);

                    Type genericType = property.PropertyType.GetGenericArguments()[0];
                    MethodInfo method = genericType.GetMethod("LoadFromDictionary",
                            BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public)
                        .MakeGenericMethod(genericType);

                    foreach (Dictionary<string, object> attr in (List<Dictionary<string, object>>)attribute)
                    {
                        ((IList)property.GetValue(resource, null)).Add(method.Invoke(resource, new object[]
                        {
                            attr
                        }));
                    }
                }
                else
                {
                    property.SetValue(resource, attribute, null);
                }
            }

            return resource;
        }
    }
}
