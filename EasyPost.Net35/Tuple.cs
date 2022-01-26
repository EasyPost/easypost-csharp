using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyPost {
    public class Tuple<T1, T2> {
        public T1 Item1 { get; private set; }
        public T2 Item2 { get; private set; }
        internal Tuple(T1 first, T2 second) {
            Item1 = first;
            Item2 = second;
        }
    }
}
