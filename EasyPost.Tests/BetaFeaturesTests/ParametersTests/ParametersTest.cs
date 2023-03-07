using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ParametersTests
{
    public class ParametersTest : UnitTest
    {
        public ParametersTest() : base("parameters")
        {
        }

        #region Tests

        /// <summary>
        ///     This test proves that the Parameters object can be serialized to a dictionary.
        /// </summary>
        [Fact]
        [Testing.Function]
        public void TestParametersToDictionary()
        {
            const string street = "388 Townsend St";

            var parameters = new EasyPost.BetaFeatures.Parameters.Addresses.Create
            {
                Street1 = street,
                Street2 = "Apt 20",
                City = "San Francisco",
                State = "CA",
                Zip = "94107",
                Country = "US",
            };

            var dictionary = parameters.ToDictionary();

            // Check that the dictionary contains "address"
            Assert.True(dictionary.ContainsKey("address"));

            // Check that the "address" sub-dictionary was created correctly
            var addressData = dictionary["address"] as Dictionary<string, object>;

            // Check that the "address" sub-dictionary contains the 6 elements we set
            Assert.True(addressData.ContainsKey("street1"));
            Assert.True(addressData.ContainsKey("street2"));
            Assert.True(addressData.ContainsKey("city"));
            Assert.True(addressData.ContainsKey("state"));
            Assert.True(addressData.ContainsKey("zip"));
            Assert.True(addressData.ContainsKey("country"));

            // Check that the "street1" key was set with the correct value
            Assert.Equal(street, addressData["street1"]);
        }

        /// <summary>
        ///     This test proves that both an EasyPostObject and a Parameters object can be provided as a parameter value.
        /// </summary>
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

            EasyPost.BetaFeatures.Parameters.Addresses.Create newAddressCreationParameters = new EasyPost.BetaFeatures.Parameters.Addresses.Create
            {
                Street1 = streetB,
            };

            // Users can pass in an existing Address object as the "ToAddress" parameter
            var parameters = new EasyPost.BetaFeatures.Parameters.Shipments.Create
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

            // Users can also pass in an Addresses.Create parameter object as the "ToAddress" parameter
            parameters = new EasyPost.BetaFeatures.Parameters.Shipments.Create
            {
                IsReturn = false,
                ToAddress = newAddressCreationParameters,
            };

            // No errors here confirm that the dictionary was serialized to the expected schema
            dictionary = parameters.ToDictionary();
            shipmentData = dictionary["shipment"] as Dictionary<string, object>;
            addressData = shipmentData["to_address"] as Dictionary<string, object>;

            // The value of "street1" should be the value of "streetB" via the Addresses.Create parameter object
            Assert.Equal(streetB, addressData["street1"]);
        }

        /// <summary>
        /// This test proves that we can reuse the Addresses.Create parameter object,
        /// with its serialization logic adapting to whether it is a top-level parameter object
        /// or a nested parameter object within another parameter object.
        ///
        /// Notice how the paths to "street1" are different depending on whether the Addresses.Create parameter object.
        ///
        /// The schema for an address creation API call contains all address data wrapped in an "address" key (excluding irrelevant "verify" and "verify_strict" keys).
        ///
        /// The schema for a shipment creation API call does not contain this "address" key, with all address data instead wrapped inside a "to_address" key.
        ///
        /// Behind the scenes, the Addresses.Create parameter object is the same, but the serialization path for each property (parameter) adapts to the context in which it is used
        ///
        /// This is powered by the NestedRequestParameter attribute, which apply a different serialization path when the parameter set is being used nested within another parameter set.
        /// </summary>
        [Fact]
        [Testing.Logic]
        public void TestTopLevelVersusNestedParameters()
        {
            const string street = "388 Townsend St";

            EasyPost.BetaFeatures.Parameters.Addresses.Create addressCreationParameters = new EasyPost.BetaFeatures.Parameters.Addresses.Create
            {
                Street1 = street,
            };

            // Using the Addresses.Create parameter object as a top-level parameter object
            var dictionary = addressCreationParameters.ToDictionary();

            // Path to "street1" should be dictionary["address"]["street1"]
            var addressData = dictionary["address"] as Dictionary<string, object>;
            Assert.Equal(street, addressData["street1"]);

            // Using the Addresses.Create parameter object as a nested parameter object
            var shipmentCreationParameters = new EasyPost.BetaFeatures.Parameters.Shipments.Create
            {
                IsReturn = false,
                ToAddress = addressCreationParameters,
            };
            dictionary = shipmentCreationParameters.ToDictionary();

            // Path to "street1" should be dictionary["shipment"]["to_address"]["street1"]
            var shipmentData = dictionary["shipment"] as Dictionary<string, object>;
            var toAddressData = shipmentData["to_address"] as Dictionary<string, object>;
            Assert.Equal(street, toAddressData["street1"]);
        }

        /// <summary>
        ///     This test proves that the overloaded Create methods, which accept a parameter object rather than a dictionary, work as expected.
        /// </summary>
        [Fact]
        [Testing.Function]
        public async Task TestAddressCreateFunctionWithParameterObject()
        {
            UseVCR("address_create_function_with_parameter_object");

            const string street = "388 Townsend St";

            EasyPost.BetaFeatures.Parameters.Addresses.Create addressCreationParameters = new EasyPost.BetaFeatures.Parameters.Addresses.Create
            {
                Street1 = street,
            };

            Address address = await Client.Address.Create(addressCreationParameters);

            // If we got this far, the API call was successful (no error was thrown)

            // Check that the "street1" key was set with the correct value (suggesting that the API received the correct data schema and was able to create the address properly)
            Assert.Equal(street, address.Street1);
        }

        /// <summary>
        ///     This test proves why we should not allow end-users to access the .ToDictionary() method for parameter objects.
        ///
        ///     If users use the .ToDictionary() method to serialize a parameter object, and then pass the resulting dictionary to the normal Create method,
        ///     the data will be double-wrapped, sending malformed data to the API and producing unexpected results.
        ///
        ///     For this reason, the .ToDictionary() method is "internal", and should only be used by the library itself.
        ///
        ///     Instead, parameter objects can only be used by passing them into the overloaded methods, which will call the .ToDictionary() method internally.
        /// </summary>
        [Fact]
        [Testing.Exception]
        public async Task TestDisallowUsingParameterObjectDictionariesInDictionaryFunctions()
        {
            UseVCR("disallow_using_parameter_object_dictionaries_in_dictionary_functions");

            const string street = "388 Townsend St";

            EasyPost.BetaFeatures.Parameters.Addresses.Create addressCreationParameters = new EasyPost.BetaFeatures.Parameters.Addresses.Create
            {
                Street1 = street,
            };

            Dictionary<string, object> dictionary = addressCreationParameters.ToDictionary();  // this method is "internal", so end-users won't have access to it, for reasons seen below

            // At this point, the data has already been wrapped properly by the .ToDictionary() method, and this method expects raw (unwrapped) data
            // This will cause a double-wrapping, sending malformed data to the API
            Address address = await Client.Address.Create(dictionary);

            // The API doesn't fail due to the malformed data, but the address was not created properly
            Assert.NotEqual(street, address.Street1);
        }

        #endregion
    }
}
