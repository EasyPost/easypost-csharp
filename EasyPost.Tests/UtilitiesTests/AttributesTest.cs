using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyPost.Models.API;
using EasyPost.Services;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;
using Xunit;

namespace EasyPost.Tests.UtilitiesTests
{
    public class AttributesTest : UnitTest
    {
        public AttributesTest() : base("attributes")
        {
        }

        #region Tests

        [Fact]
        [Testing.Function]
        public void TestGetMethodsWithAttributes()
        {
            UseMockClient();

            // via type
            IEnumerable<MethodInfo> methods = BaseCustomAttribute.GetMethodsWithAttribute<CrudOperations.Update>(typeof(ShipmentService));
            Assert.True(methods.Any());

            // via instance
            ShipmentService shipmentService = Client.Shipment;
            methods = BaseCustomAttribute.GetMethodsWithAttribute<CrudOperations.Update>(shipmentService);
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
