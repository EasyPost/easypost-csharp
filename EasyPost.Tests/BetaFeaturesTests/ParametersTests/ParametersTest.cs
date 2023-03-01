using System.Collections.Generic;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ParametersTests
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

            var parameters = new Addresses.Create
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

            // Check that the dictionary contains "address" + default "verify" and "verify_strict" top-level keys
            Assert.True(dictionary.ContainsKey("address"));
            Assert.True(dictionary.ContainsKey("verify"));
            Assert.True(dictionary.ContainsKey("verify_strict"));

            // Check that the "address" sub-dictionary was created correctly
            var addressData = dictionary["address"] as Dictionary<string, object>;
            Assert.NotNull(addressData);

            // Check that the "address" sub-dictionary contains the 6 elements we set + default "residential" boolean
            Assert.True(addressData.ContainsKey("street1"));
            Assert.True(addressData.ContainsKey("street2"));
            Assert.True(addressData.ContainsKey("city"));
            Assert.True(addressData.ContainsKey("state"));
            Assert.True(addressData.ContainsKey("zip"));
            Assert.True(addressData.ContainsKey("country"));
            Assert.True(addressData.ContainsKey("residential"));

            // Check that the "street1" key was set with the correct value
            Assert.NotNull(addressData["street1"]);
            Assert.Equal(street, addressData["street1"]);
        }

        [Fact]
        [Testing.Parameters]
        public void TestParametersToDictionaryWithSubDictionary()
        {
            const string streetA = "388 Townsend St";
            const string streetB = "123 Main St";

            Address preExistingAddressObject = new Address
            {
                Street1 = streetA,
            };

            Addresses.Create newAddressCreationParameters = new Addresses.Create
            {
                Street1 = streetB,
            };

            // Users can pass in an existing Address object as the "ToAddress" parameter
            var parameters = new Shipments.Create
            {
                IsReturn = false,
                ToAddress = preExistingAddressObject,
            };

            // No errors here confirm that the dictionary was serialized to the expected schema
            var dictionary = parameters.ToDictionary();
            var shipmentData = dictionary["shipment"] as Dictionary<string, object>;
            var addressData = shipmentData["to_address"] as Dictionary<string, object>;

            // The value of "street1" should be the value of "streetA" via the Address object
            Assert.Equal(streetA, addressData["street1"]);

            // Users can also pass in an Address.Create parameter object as the "ToAddress" parameter
            parameters = new Shipments.Create
            {
                IsReturn = false,
                ToAddress = newAddressCreationParameters,
            };

            // No errors here confirm that the dictionary was serialized to the expected schema
            dictionary = parameters.ToDictionary();
            shipmentData = dictionary["shipment"] as Dictionary<string, object>;
            addressData = shipmentData["to_address"] as Dictionary<string, object>;

            // The value of "street1" should be the value of "streetB" via the Address.Create parameter object
            Assert.Equal(streetB, addressData["street1"]);
        }

        [Fact]
        [Testing.Logic]
        public void TestTopLevelVersusNestedParameters()
        {
            // This test proves that we can reuse the Address.Create parameter object,
            // with its serialization logic adapting to whether it is
            // a top-level parameter object or a nested parameter object within another parameter object.

            const string street = "388 Townsend St";

            Addresses.Create addressCreationParameters = new Addresses.Create
            {
                Street1 = street,
            };

            // Using the Address.Create parameter object as a top-level parameter object
            var dictionary = addressCreationParameters.ToDictionary();

            // Path to "street1" should be dictionary["address"]["street1"]
            var addressData = dictionary["address"] as Dictionary<string, object>;
            Assert.Equal(street, addressData["street1"]);

            // Using the Address.Create parameter object as a nested parameter object
            var shipmentCreationParameters = new Shipments.Create
            {
                IsReturn = false,
                ToAddress = addressCreationParameters,
            };
            dictionary = shipmentCreationParameters.ToDictionary();

            // Path to "street1" should be dictionary["shipment"]["to_address"]["street1"]
            var shipmentData = dictionary["shipment"] as Dictionary<string, object>;
            var toAddressData = shipmentData["to_address"] as Dictionary<string, object>;
            Assert.Equal(street, toAddressData["street1"]);

            // Notice how the paths to "street1" are different depending on whether the Address.Create parameter object
            // The schema for an address creation API call contains all address data wrapped in an "address" key (excluding irrelevant "verify" and "verify_strict" keys)
            // The schema for a shipment creation API call does not contain this "address" key, with all address data instead wrapped inside a "to_address" key
            // Behind the scenes, the Address.Create parameter object is the same, but the serialization path for each property (parameter) adapts to the context in which it is used
        }

        #endregion
    }
}
