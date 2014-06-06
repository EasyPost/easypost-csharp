using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    internal static class Resource {
        public static void Merge(this object dest, object source) {
            foreach (PropertyInfo property in source.GetType().GetProperties()) {
                property.SetValue(dest, property.GetValue(source));
            }
        }
    }
}
