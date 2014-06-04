using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EasyPost {
    class Resource {
        public static void Merge(object left, object right) {
            foreach (PropertyInfo property in right.GetType().GetProperties()) {
                property.SetValue(left, property.GetValue(right));
            }
        }
    }
}
