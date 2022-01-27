// Tuple.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

namespace EasyPost.Net35
{
    public class Tuple<T1, T2>
    {
        public T1 Item1 { get; }

        public T2 Item2 { get; }

        internal Tuple(T1 first, T2 second)
        {
            Item1 = first;
            Item2 = second;
        }
    }
}
