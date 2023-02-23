using System.Collections.Generic;
using EasyPost.Tests._Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ParametersTests
{
    public class ParametersTest
    {
        public ParametersTest()
        {
        }

        #region Tests

        [Fact]
        [Testing.Function]
        public void TestParametersToDictionary()
        {
            const string street = "388 Townsend St";

            var parameters = new Parameters.V2.Addresses.Create
            {
                Street1 = street,
                Street2 = "Apt 20",
                City = "San Francisco",
                State = "CA",
                Zip = "94107",
                Country = "US",
            };

            var dictionary = parameters.ToDictionary();

            // Check that a dictionary was created correctly
            Assert.NotNull(dictionary);

            // Check that the dictionary contains 3 elements, including an "address" key
            Assert.Equal(3, dictionary.Count); // "address" + default "verify" and "verify_strict" top-level keys
            Assert.True(dictionary.ContainsKey("address"));

            // Check that the value of the "address" key is a dictionary with 7 elements
            var addressData = dictionary["address"] as Dictionary<string, object>;
            Assert.NotNull(addressData);
            Assert.Equal(7, addressData.Count); // the 6 elements we set + default "residential" boolean

            // Check that the "address" dictionary contains a key "street1" with the correct value
            Assert.True(addressData.ContainsKey("street1"));
            Assert.NotNull(addressData["street1"]);
            Assert.Equal(street, addressData["street1"]);
        }

        [Fact(Skip = "Embedded sub-parameters are not yet implemented properly")]
        [Testing.EdgeCase]
        public void TestParametersToDictionaryWithSubParameters()
        {
            const string street = "388 Townsend St";

            var parameters = new Parameters.V2.Shipments.Create
            {
                IsReturn = false,
                ToAddressParameters = new Parameters.V2.Addresses.Create
                {
                    Street1 = street,
                    Street2 = "Apt 20",
                    City = "San Francisco",
                    State = "CA",
                    Zip = "94107",
                    Country = "US",
                },
            };

            var dictionary = parameters.ToDictionary();

            // Check that a dictionary was created correctly
            Assert.NotNull(dictionary);

            // Check that the dictionary contains 2 elements, including an "shipment" key
            Assert.Equal(2, dictionary.Count); // "shipment" + default "carbon_offset" top-level key
            Assert.True(dictionary.ContainsKey("shipment"));

            // Check that the value of the "shipment" key is a dictionary with 2 elements
            var shipmentData = dictionary["shipment"] as Dictionary<string, object>;
            Assert.NotNull(shipmentData);
            Assert.Equal(3, shipmentData.Count); // the 1 element we set + default "is_return" boolean and "insurance" 0 value

            // TODO: This test fails because address data will get embedded as "to_address" -> "address" -> "street1" instead of "to_address" -> "street1"

            // Check that the value of the "to_address" key is a dictionary with 7 elements
            var addressData = shipmentData["to_address"] as Dictionary<string, object>;
            Assert.NotNull(addressData);
            Assert.Equal(7, addressData.Count); // the 6 elements we set + default "residential" boolean

            // Check that the "to_address" dictionary contains a key "street1" with the correct value
            Assert.True(addressData.ContainsKey("street1"));
            Assert.NotNull(addressData["street1"]);
            Assert.Equal(street, addressData["street1"]);
        }


        #endregion
    }
}
