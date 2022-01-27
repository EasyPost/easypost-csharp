// LayoutExample.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

// This is an example class file to demonstrate a file header (above) and the layout and order of a class file (below).
namespace EasyPost
{
    public class LayoutExample
    {
        // Constants
        // Access -> Alphabetically
        public const string const1 = "const1";

        internal const string const2 = "const2";

        protected const string const3 = "const3";

        private const string const4 = "const4";

        // Enums
        // Access -> Alphabetically
        public enum enum1
        {
        }

        internal enum enum2
        {
        }

        protected enum enum3
        {
        }

        private enum enum4
        {
        }

        // Fields
        // Readonly (yes -> no) -> Access -> Alphabetically
        public readonly int field1;

        internal readonly int field2;

        protected readonly int field3;

        private readonly int field4;

        public int field5;

        internal int field6;

        protected int field7;

        private int field8;


        // Properties
        // Static (no -> yes) -> Access -> Alphabetically
        public int property1 { get; set; }

        internal int property2 { get; set; }

        protected int property3 { get; set; }

        private int property4 { get; set; }

        public static int property5 { get; set; }

        internal static int property6 { get; set; }

        protected static int property7 { get; set; }

        private static int property8 { get; set; }

        // Constructors
        // Access -> Alphabetically
        public Example() // constructor 1
        {
        }

        protected Example(bool var) // constructor 3
        {
        }

        internal Example(int var) // constructor 2
        {
        }

        private Example(string var) // constructor 4
        {
        }

        // Methods
        // Static (no -> yes) -> Access -> Alphabetically
        public void method1()
        {
        }

        internal void method2()
        {
        }

        protected void method3()
        {
        }

        private void method4()
        {
        }

        public static void method5()
        {
        }

        internal static void method6()
        {
        }

        protected static void method7()
        {
        }

        private static void method8()
        {
        }
    }
}
