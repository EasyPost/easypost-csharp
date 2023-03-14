using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.UtilitiesTests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
    internal sealed class ExampleAttribute : BaseCustomAttribute
    {
    }

#pragma warning disable CA1852 // Can be sealed
    internal class ExampleClass
    {
        [Example]
        public string? PublicProperty { get; set; }
        [Example]
        internal string? InternalProperty { get; set; }
        [Example]
        protected string? ProtectedProperty { get; set; }
        [Example]
        private string? PrivateProperty { get; set; }

        [Example]
        public static string? PublicStaticProperty { get; set; }
        [Example]
        internal static string? InternalStaticProperty { get; set; }
        [Example]
        protected static string? ProtectedStaticProperty { get; set; }
        [Example]
        private static string? PrivateStaticProperty { get; set; }

        [Example]
        public void PublicMethod()
        {
        }

        [Example]
        internal void InternalMethod()
        {
        }

        [Example]
        protected void ProtectedMethod()
        {
        }

        [Example]
        private void PrivateMethod()
        {
        }

        [Example]
        public static void PublicStaticMethod()
        {
        }

        [Example]
        internal static void InternalStaticMethod()
        {
        }

        [Example]
        protected static void ProtectedStaticMethod()
        {
        }

        [Example]
        private static void PrivateStaticMethod()
        {
        }
    }
#pragma warning restore CA1852 // Can be sealed

    public class AttributesTest
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestGetMethodsWithAttributes()
        {
            // via type
            IEnumerable<MethodInfo> methods = BaseCustomAttribute.GetMethodsWithAttribute<ExampleAttribute>(typeof(ExampleClass));
            Assert.True(methods.Any());

            // via instance
            ExampleClass exampleClass = new();
            methods = BaseCustomAttribute.GetMethodsWithAttribute<ExampleAttribute>(exampleClass);
            Assert.True(methods.Any());
        }

        [Fact]
        [Testing.Logic]
        public void TestCanGetNonPublicAndStaticMethodsWithAttributes()
        {
            // via type
            IEnumerable<MethodInfo> methods = BaseCustomAttribute.GetMethodsWithAttribute<ExampleAttribute>(typeof(ExampleClass));
            // we should have 8 methods with the attribute
            Assert.True(methods.Count() == 8);

            // via instance
            ExampleClass exampleClass = new();
            methods = BaseCustomAttribute.GetMethodsWithAttribute<ExampleAttribute>(exampleClass);
            // we should have 8 methods with the attribute
            Assert.True(methods.Count() == 8);
        }

        [Fact]
        [Testing.Function]
        public void TestGetPropertiesWithAttributes()
        {
            // via type
            IEnumerable<PropertyInfo> properties = BaseCustomAttribute.GetPropertiesWithAttribute<ExampleAttribute>(typeof(ExampleClass));
            Assert.True(properties.Any());

            // via instance
            ExampleClass exampleClass = new();
            properties = BaseCustomAttribute.GetPropertiesWithAttribute<ExampleAttribute>(exampleClass);
            Assert.True(properties.Any());
        }

        [Fact]
        [Testing.Logic]
        public void TestCanGetNonPublicAndStaticPropertiesWithAttributes()
        {
            // via type
            IEnumerable<PropertyInfo> properties = BaseCustomAttribute.GetPropertiesWithAttribute<ExampleAttribute>(typeof(ExampleClass));
            // we should have 8 properties with the attribute
            Assert.True(properties.Count() == 8);

            // via instance
            ExampleClass exampleClass = new();
            properties = BaseCustomAttribute.GetPropertiesWithAttribute<ExampleAttribute>(exampleClass);
            // we should have 8 properties with the attribute
            Assert.True(properties.Count() == 8);
        }

        #endregion
    }
}
