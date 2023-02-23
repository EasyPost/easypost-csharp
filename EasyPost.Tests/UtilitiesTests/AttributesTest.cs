using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Internal.Annotations;
using Newtonsoft.Json;
using Xunit;

namespace EasyPost.Tests.UtilitiesTests
{
    public class AttributesTest
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestGetMethodsWithAttributes()
        {
            // via type
            IEnumerable<MethodInfo> methods = BaseCustomAttribute.GetMethodsWithAttribute<CrudOperations.Update>(typeof(Shipment));
            Assert.True(methods.Any());

            // via instance
            Shipment shipment = new();
            methods = BaseCustomAttribute.GetMethodsWithAttribute<CrudOperations.Update>(shipment);
            Assert.True(methods.Any());
        }

        [Fact]
        [Testing.Function]
        public void TestGetPropertiesWithAttributes()
        {
            // via type
            IEnumerable<PropertyInfo> properties = BaseCustomAttribute.GetPropertiesWithAttribute<JsonPropertyAttribute>(typeof(Shipment));
            Assert.True(properties.Any());

            // via instance
            Shipment shipment = new();
            properties = BaseCustomAttribute.GetPropertiesWithAttribute<JsonPropertyAttribute>(shipment);
            Assert.True(properties.Any());
        }

        #endregion
    }
}
