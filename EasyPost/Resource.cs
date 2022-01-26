using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyPost
{
    public class Resource : IResource
    {
        /// <summary>
        /// Deserialize a JSON string into an object instance.
        /// </summary>
        /// <param name="json">The JSON to deserialize.</param>
        /// <typeparam name="T">The type of object to generate.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        public static T Load<T>(string json) where T : Resource
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Load a dictionary of properties into an object instance.
        /// </summary>
        /// <param name="attributes">A dictionary of key-value pairs of attributes for the object instance.</param>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        public static T LoadFromDictionary<T>(Dictionary<string, object> attributes) where T : Resource
        {
            Type type = typeof(T);
            T resource = (T)Activator.CreateInstance(type);

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (attributes.TryGetValue(property.Name, out object attribute) == false)
                    continue;

                if (property.PropertyType.GetInterfaces().Contains(typeof(IResource)))
                {
                    MethodInfo method = property.PropertyType
                        .GetMethod("LoadFromDictionary",
                            BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public)
                        .MakeGenericMethod(new Type[] { property.PropertyType });

                    property.SetValue(resource, method.Invoke(resource, new object[] { attribute }), null);
                }
                else if (typeof(IEnumerable<IResource>).IsAssignableFrom(property.PropertyType))
                {
                    property.SetValue(resource, Activator.CreateInstance(property.PropertyType), null);

                    Type genericType = property.PropertyType.GetGenericArguments()[0];
                    MethodInfo method = genericType.GetMethod("LoadFromDictionary",
                            BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public)
                        .MakeGenericMethod(new Type[] { genericType });

                    foreach (Dictionary<string, object> attr in (List<Dictionary<string, object>>)attribute)
                    {
                        ((IList)property.GetValue(resource, null)).Add(method.Invoke(resource, new object[] { attr }));
                    }
                }
                else
                {
                    property.SetValue(resource, attribute, null);
                }
            }

            return resource;
        }

        /// <summary>
        /// Merge the properties of this object instance with the properties of another object instance.
        /// Adds properties from the input object instance into this object instance.
        /// </summary>
        /// <param name="source">Object instance to extract properties from to merge into this object instance.</param>
        public void Merge(object source)
        {
            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(source, null), null);
            }
        }

        /// <summary>
        /// Get the dictionary representation of this object instance.
        /// </summary>
        /// <returns>A key-value dictionary representation of this object instance's attributes.</returns>
        public Dictionary<string, object> AsDictionary()
        {
            return this.GetType()
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(info => info.Name, info => GetValue(info));
        }

        private object GetValue(PropertyInfo info)
        {
            object value = info.GetValue(this, null);

            if (value is IResource)
            {
                return ((IResource)value).AsDictionary();
            }
            else if (value is IEnumerable<IResource>)
            {
                List<Dictionary<string, object>> values = new List<Dictionary<string, object>>();

                foreach (IResource IResource in (IEnumerable<IResource>)value)
                {
                    values.Add(IResource.AsDictionary());
                }

                return values;
            }
            else
            {
                return value;
            }
        }
    }
}