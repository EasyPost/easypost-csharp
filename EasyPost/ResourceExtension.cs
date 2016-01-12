using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyPost {
    internal static class ResourceExtension {
        public static void Merge(this IResource dest, object source) {
            foreach (PropertyInfo property in source.GetType().GetProperties()) {
                property.SetValue(dest, property.GetValue(source, null), null);
            }
        }

        public static Dictionary<string, object> AsDictionary(this IResource source) {
            return source.GetType()
                         .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                         .ToDictionary(info => info.Name, info => GetValue(source, info));
        }

        private static object GetValue(IResource source, PropertyInfo info) {
            object value = info.GetValue(source, null);

            if (value is IResource) {
                return ((IResource)value).AsDictionary();
            } else if (value is IEnumerable<IResource>) {
                List<Dictionary<string, object>> values = new List<Dictionary<string, object>>();
                foreach (IResource resource in (IEnumerable<IResource>)value) {
                    values.Add(((IResource)resource).AsDictionary());
                }
                return (object)values;
            } else {
                return value;
            }
        }
    }
}
