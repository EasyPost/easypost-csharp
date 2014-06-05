using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    internal class Resource {
        public static void Merge(object left, object right) {
            foreach (PropertyInfo property in right.GetType().GetProperties()) {
                property.SetValue(left, property.GetValue(right));
            }
        }
    }
}
