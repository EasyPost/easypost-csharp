using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ParametersTests
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

            Parameters.Address.Create parameters = new()
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

            Parameters.Address.Create newAddressCreationParameters = new()
            {
                Street1 = streetB,
            };

            // Users can pass in an existing Address object as the "ToAddress" parameter
            var parameters = new Parameters.Shipment.Create
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
            parameters = new Parameters.Shipment.Create
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
        ///     This test proves that you can reuse a parameter object, and that re-serializing it will take into account any changes made to its properties since the last serialization.
        /// </summary>
        [Fact]
        [Testing.Logic]
        public void TestReusingParameterSets()
        {
            var parameters = new Parameters.Shipment.All
            {
                BeforeId = null,
            };

            // null values should not be serialized
            var parametersDictionary = parameters.ToDictionary();
            Assert.False(parametersDictionary.ContainsKey("before_id"));

            parameters.BeforeId = "1";

            // now that the property has a value, it should be serialized
            parametersDictionary = parameters.ToDictionary();
            Assert.True(parametersDictionary.ContainsKey("before_id"));
            Assert.Equal("1", parametersDictionary["before_id"]);

            parameters.BeforeId = "2";

            // the new value should be serialized
            parametersDictionary = parameters.ToDictionary();
            Assert.True(parametersDictionary.ContainsKey("before_id"));
            Assert.Equal("2", parametersDictionary["before_id"]);

            parameters.BeforeId = null;

            // null values should not be serialized
            parametersDictionary = parameters.ToDictionary();
            Assert.False(parametersDictionary.ContainsKey("before_id"));
        }

        [Fact]
        [Testing.Logic]
        public void TestCompetingParameters()
        {
            var parametersWithCompetingParameters = new ParameterSetWithCompetingParameters
            {
                AParam = "location1",
                BParam = "location2",
            };

            // Both values are serializing to the same location ("location") in the dictionary, so which wins?
            var dictionary = parametersWithCompetingParameters.ToDictionary();

            // It seems that BParam here wins.
            Assert.Equal("location2", dictionary["location"]);

            // Is it because the properties are serialized in alphabetical order, or because BParam is the last property in the code structure?
            // Let's test by reversing the order of the properties.
            var parametersWithCompetingParametersNonAlphabetic = new ParameterSetWithCompetingParametersNonAlphabetic
            {
                // The order the properties are set in the constructor shouldn't matter.
                // In the source code, BParam physically comes before AParam.
                // We'll replicate that order here just for readability sake
                BParam = "location1",
                AParam = "location2",
            };

            var dictionaryNonAlphabetic = parametersWithCompetingParametersNonAlphabetic.ToDictionary();
            Assert.Equal("location2", dictionaryNonAlphabetic["location"]);

            // Just one last confirmation, let's keep the flipped alphabetical order, but rule out that the constructor order matters.
            var parametersWithCompetingParametersNonAlphabetic2 = new ParameterSetWithCompetingParametersNonAlphabetic
            {
                // Again, AParam is physically located after BParam in the code structure, but it's set first in the constructor here.
                AParam = "location2",
                BParam = "location1",
            };

            var dictionaryNonAlphabetic2 = parametersWithCompetingParametersNonAlphabetic2.ToDictionary();
            Assert.Equal("location2", dictionaryNonAlphabetic2["location"]);

            // The constructor order doesn't seem to matter (which is a good thing because we can't control in what order end-users will set the properties).

            // It seems the properties are in fact serialized not in alphabetical order, but in the order they are defined in the code structure.
            // If you need to add an override to a parameter, physically place it after the parameter it overrides in the code structure, to ensure it is serialized last.

            // The downside to this is linting. Our linter rules like to order properties alphabetically in the code structure.
            // Meaning, to ensure that the override parameter is physically below the parameter it overrides, we either have to disable the linter rule for that file,
            // or the override parameter has to be alphabetically after the parameter it overrides, which limits us on naming choices.
        }

        [Fact]
        [Testing.Exception]
        public void TestRequiredAndOptionalParameterValidation()
        {
            var parametersWithBothParameterSet = new ParameterSetWithRequiredAndOptionalParameters
            {
                RequiredParameter = "required",
                OptionalParameter = "optional",
            };
            // should not throw an exception when serializing to a dictionary
            parametersWithBothParameterSet.ToDictionary();

            var parametersWithOnlyRequiredParameterSet = new ParameterSetWithRequiredAndOptionalParameters
            {
                RequiredParameter = "required",
            };

            // should not throw an exception when serializing to a dictionary
            parametersWithOnlyRequiredParameterSet.ToDictionary();

            var parametersWithOnlyOptionalParameterSet = new ParameterSetWithRequiredAndOptionalParameters
            {
                OptionalParameter = "optional",
            };

            // should throw an exception when serializing to a dictionary
            Assert.Throws<Exceptions.General.MissingParameterError>(() => parametersWithOnlyOptionalParameterSet.ToDictionary());
        }

        [Fact]
        [Testing.Exception]
        public void TestOneOrOtherDependentPresenceBasedTopLevelParameters()
        {
            // Either A or B must be set, but not both.

            // Should throw exception if both set.
            var parametersWithOneOrOtherInterdependenceBothSet = new ParameterSetWithOneOrOtherDependentPresenceBasedTopLevelParameters
            {
                AParam = "A",
                BParam = "B",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithOneOrOtherInterdependenceBothSet.ToDictionary());

            // Should throw exception if neither set.
            var parametersWithOneOrOtherInterdependenceNeitherSet = new ParameterSetWithOneOrOtherDependentPresenceBasedTopLevelParameters();

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithOneOrOtherInterdependenceNeitherSet.ToDictionary());

            // Should not throw exception if only A is set.
            var parametersWithOneOrOtherInterdependenceOnlyASet = new ParameterSetWithOneOrOtherDependentPresenceBasedTopLevelParameters
            {
                AParam = "A",
            };

            try
            {
                parametersWithOneOrOtherInterdependenceOnlyASet.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if only A is set.");
            }

            // Should not throw exception if only B is set.
            var parametersWithOneOrOtherInterdependenceOnlyBSet = new ParameterSetWithOneOrOtherDependentPresenceBasedTopLevelParameters
            {
                BParam = "B",
            };

            try
            {
                parametersWithOneOrOtherInterdependenceOnlyBSet.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if only B is set.");
            }
        }

        [Fact]
        [Testing.Exception]
        public void TestBothOrNeitherDependentPresenceBasedTopLevelParameters()
        {
            // Either both A and B must be set, or neither.

            // Should throw exception if only A is set.
            var parametersWithBothOrNeitherInterdependenceOnlyASet = new ParameterSetWithBothOrNeitherDependentPresenceBasedTopLevelParameters
            {
                AParam = "A",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithBothOrNeitherInterdependenceOnlyASet.ToDictionary());

            // Should throw exception if only B is set.
            var parametersWithBothOrNeitherInterdependenceOnlyBSet = new ParameterSetWithBothOrNeitherDependentPresenceBasedTopLevelParameters
            {
                BParam = "B",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithBothOrNeitherInterdependenceOnlyBSet.ToDictionary());

            // Should not throw exception if both A and B are set.
            var parametersWithBothOrNeitherInterdependenceBothSet = new ParameterSetWithBothOrNeitherDependentPresenceBasedTopLevelParameters
            {
                AParam = "A",
                BParam = "B",
            };

            try
            {
                parametersWithBothOrNeitherInterdependenceBothSet.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if both A and B are set.");
            }

            // Should not throw exception if neither A nor B are set.
            var parametersWithBothOrNeitherInterdependenceNeitherSet = new ParameterSetWithBothOrNeitherDependentPresenceBasedTopLevelParameters();

            try
            {
                parametersWithBothOrNeitherInterdependenceNeitherSet.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if neither A nor B are set.");
            }
        }

        [Fact]
        [Testing.Exception]
        public void TestOneOrOtherDependentValueBasedTopLevelParameters()
        {
            // If AParm is set to "A", BParam must be set to "B", and vice versa.

            // Should throw exception if B is not set properly based on A set restriction.
            var parametersWithOneOrOtherInterdependenceBothSet = new ParameterSetWithOneOrOtherDependentValueBasedTopLevelParameters
            {
                AParam = "A",
                BParam = "NOT B",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithOneOrOtherInterdependenceBothSet.ToDictionary());

            // Should throw exception if A is not set properly based on B set restriction.
            var parametersWithOneOrOtherInterdependenceBothSet2 = new ParameterSetWithOneOrOtherDependentValueBasedTopLevelParameters
            {
                AParam = "NOT A",
                BParam = "B",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithOneOrOtherInterdependenceBothSet2.ToDictionary());

            // Should not throw exception if both A and B are set properly.
            var parametersWithOneOrOtherInterdependenceBothSet3 = new ParameterSetWithOneOrOtherDependentValueBasedTopLevelParameters
            {
                AParam = "A",
                BParam = "B",
            };

            try
            {
                parametersWithOneOrOtherInterdependenceBothSet3.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if both A and B are set properly.");
            }

            // Should not throw exception if neither A nor B are set according to their restrictions.
            var parametersWithOneOrOtherInterdependenceNeitherSet = new ParameterSetWithOneOrOtherDependentValueBasedTopLevelParameters
            {
                AParam = "NOT A",
                BParam = "NOT B",
            };

            try
            {
                parametersWithOneOrOtherInterdependenceNeitherSet.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if neither A nor B are set according to their restrictions.");
            }
        }

        [Fact]
        [Testing.Exception]
        public void TestBothOrNeitherDependentValueBasedTopLevelParameters()
        {
            // If AParm is set to "A", BParam must be set to "B", and vice versa.

            // Should throw exception if A and B are set to their competing values.
            var parametersWithOneOrOtherInterdependenceBothSet = new ParameterSetWithBothOrNeitherDependentValueBasedTopLevelParameters
            {
                AParam = "A",
                BParam = "B",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithOneOrOtherInterdependenceBothSet.ToDictionary());

            // Should throw exception if A and B are both not set to their competing values.
            var parametersWithOneOrOtherInterdependenceBothSet2 = new ParameterSetWithBothOrNeitherDependentValueBasedTopLevelParameters
            {
                AParam = "NOT A",
                BParam = "NOT B",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithOneOrOtherInterdependenceBothSet2.ToDictionary());

            // Should not throw exception if only A is set.
            var parametersWithOneOrOtherInterdependenceOnlyASet = new ParameterSetWithBothOrNeitherDependentValueBasedTopLevelParameters
            {
                AParam = "A",
            };

            try
            {
                parametersWithOneOrOtherInterdependenceOnlyASet.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if only A is set.");
            }

            // Should not throw exception if only B is set.
            var parametersWithOneOrOtherInterdependenceOnlyBSet = new ParameterSetWithBothOrNeitherDependentValueBasedTopLevelParameters
            {
                BParam = "B",
            };

            try
            {
                parametersWithOneOrOtherInterdependenceOnlyBSet.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if only B is set.");
            }

            // Should throw exception if neither A nor B are set.
            var parametersWithOneOrOtherInterdependenceNeitherSet = new ParameterSetWithBothOrNeitherDependentValueBasedTopLevelParameters();

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithOneOrOtherInterdependenceNeitherSet.ToDictionary());
        }

        [Fact]
        [Testing.Exception]
        public void TestMixedValueAndPresenceBasedDependentTopLevelParameters()
        {
            // If AParam is set to "A", BParam must be set to any value.
            // If AParam is set, BParam must be set to "B".
            // Therefore, if AParam is set to "A", BParam must be set to "B".
            var shouldPass = new ParameterSetWithMixedDependentPresenceAndValueBasedTopLevelParameters
            {
                AParam = "A",
                BParam = "B",
            };

            try
            {
                shouldPass.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if both A and B are set properly.");
            }

            var shouldFail = new ParameterSetWithMixedDependentPresenceAndValueBasedTopLevelParameters
            {
                AParam = "A",
                BParam = "NOT B",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => shouldFail.ToDictionary());

            var shouldFail2 = new ParameterSetWithMixedDependentPresenceAndValueBasedTopLevelParameters
            {
                AParam = "NOT A",
                BParam = "B",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => shouldFail2.ToDictionary());

            var shouldFail3 = new ParameterSetWithMixedDependentPresenceAndValueBasedTopLevelParameters
            {
                AParam = "NOT A",
                BParam = "NOT B",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => shouldFail3.ToDictionary());

            var shouldFail4 = new ParameterSetWithMixedDependentPresenceAndValueBasedTopLevelParameters
            {
                AParam = "A",
                BParam = null,
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => shouldFail4.ToDictionary());

            // If AParam is not set to "A", BParam must not be set.
            // If AParam is not set, BParam must not be set to "B".
            // Therefore, if AParam is not set, BParam must not be set.
            var shouldPass5 = new ParameterSetWithMixedDependentPresenceAndValueBasedTopLevelParameters
            {
                AParam = null,
                BParam = null,
            };

            try
            {
                shouldPass5.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if neither A nor B are set.");
            }
        }


        [Fact]
        [Testing.Exception]
        public void TestDependentNestedParameters()
        {
            // Either A or B must be set, but not both.

            // Should throw exception if both set.
            var parametersWithInterdependenceBothSet = new ParameterSetWithOneOrOtherDependentPresenceBasedTopLevelParameters
            {
                AParam = "A",
                BParam = "B",
            };

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithInterdependenceBothSet.ToDictionary());

            // Should throw exception if neither set.
            var parametersWithInterdependenceNeitherSet = new ParameterSetWithOneOrOtherDependentPresenceBasedTopLevelParameters();

            Assert.Throws<Exceptions.General.InvalidParameterPairError>(() => parametersWithInterdependenceNeitherSet.ToDictionary());

            // Should not throw exception if only A is set.
            var parametersWithInterdependenceOnlyASet = new ParameterSetWithOneOrOtherDependentPresenceBasedTopLevelParameters
            {
                AParam = "A",
            };

            try
            {
                parametersWithInterdependenceOnlyASet.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if only A is set.");
            }

            // Should not throw exception if only B is set.
            var parametersWithInterdependenceOnlyBSet = new ParameterSetWithOneOrOtherDependentPresenceBasedTopLevelParameters
            {
                BParam = "B",
            };

            try
            {
                parametersWithInterdependenceOnlyBSet.ToDictionary();
            }
            catch (Exceptions.General.InvalidParameterPairError)
            {
                Assert.Fail("Should not throw exception if only B is set.");
            }
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

            Parameters.Address.Create addressCreationParameters = new()
            {
                Street1 = street,
            };

            // Using the Addresses.Create parameter object as a top-level parameter object
            var dictionary = addressCreationParameters.ToDictionary();

            // Path to "street1" should be dictionary["address"]["street1"]
            var addressData = dictionary["address"] as Dictionary<string, object>;
            Assert.Equal(street, addressData["street1"]);

            // Using the Addresses.Create parameter object as a nested parameter object
            var shipmentCreationParameters = new Parameters.Shipment.Create
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

            Parameters.Address.Create addressCreationParameters = new()
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

            Parameters.Address.Create addressCreationParameters = new()
            {
                Street1 = street,
            };

            Dictionary<string, object> dictionary = addressCreationParameters.ToDictionary(); // this method is "internal", so end-users won't have access to it, for reasons seen below

            // At this point, the data has already been wrapped properly by the .ToDictionary() method, and this method expects raw (unwrapped) data
            // This will cause a double-wrapping, sending malformed data to the API
            Address address = await Client.Address.Create(dictionary);

            // The API doesn't fail due to the malformed data, but the address was not created properly
            Assert.NotEqual(street, address.Street1);
        }

        /// <summary>
        ///     This test proves that the .ToDictionary() method will serialize all properties, regardless of their access modifier, as long as they are decorated with the TopLevelRequestParameter attribute.
        /// </summary>
        [Fact]
        [Testing.Logic]
        public void TestParameterToDictionaryAccountsForNonPublicProperties()
        {
            ExampleDecoratorParameters exampleDecoratorParameters = new ExampleDecoratorParameters();

            Dictionary<string, object> dictionary = exampleDecoratorParameters.ToDictionary();

            // All decorated properties should be present in the dictionary, regardless of their access modifier
            Assert.True(dictionary.ContainsKey("decorated_public_property"));
            Assert.True(dictionary.ContainsKey("decorated_internal_property"));
            Assert.True(dictionary.ContainsKey("decorated_protected_property"));
            Assert.True(dictionary.ContainsKey("decorated_private_property"));

            // None of the undecorated properties should be present in the dictionary
            // If there's only four keys in the dictionary, it means only the decorated properties (above) are present
            Assert.True(dictionary.Count == 4);
        }

        /// <summary>
        ///     This test proves that the .Matches() method will evaluate if a provided EasyPostObject matches the current parameter set, based on the defined match function.
        /// </summary>
        [Fact]
        [Testing.Logic]
        public void TestParameterMatchOverrideFunction()
        {
            ExampleMatchParametersEasyPostObject obj = new ExampleMatchParametersEasyPostObject
            {
                Prop1 = "prop1",
                // uses default match function at base level (returns false)
                // this can also be implemented on a per-parameter set basis
                // users can also override the match function to implement custom logic (see examples below)
            };

            // The default match function should return false
            ExampleMatchParameters parameters = new ExampleMatchParameters
            {
                Prop1 = "prop1",
            };
            Assert.False(parameters.Matches(obj));

            // The overridden match function should return true (because the Prop1 property matches)
            parameters = new ExampleMatchParameters
            {
                Prop1 = "prop1",
                MatchFunction = o => o.Prop1 == "prop1",
            };
            Assert.True(parameters.Matches(obj));

            // The overridden match function should return false (because the Prop1 property does not match)
            parameters = new ExampleMatchParameters
            {
                Prop1 = "prop2",
                MatchFunction = o => o.Prop1 == "prop2",
            };
            Assert.False(parameters.Matches(obj));
        }

        #endregion
    }

    #region Fixtures

#pragma warning disable CA1852 // Can be sealed
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    internal class ExampleDecoratorParameters : Parameters.BaseParameters<EasyPostObject>
    {
        // Default values set to guarantee any property won't be skipped for serialization due to a null value

        [TopLevelRequestParameter(Necessity.Optional, "decorated_public_property")]
        public string? DecoratedPublicProperty { get; set; } = "decorated_public";
        [TopLevelRequestParameter(Necessity.Optional, "decorated_internal_property")]
        internal string? DecoratedInternalProperty { get; set; } = "decorated_internal";
        [TopLevelRequestParameter(Necessity.Optional, "decorated_protected_property")]
        protected string? DecoratedProtectedProperty { get; set; } = "decorated_protected";
        [TopLevelRequestParameter(Necessity.Optional, "decorated_private_property")]
        private string? DecoratedPrivateProperty { get; set; } = "decorated_private";

        public string? UndecoratedPublicProperty { get; set; } = "undecorated_public";

        internal string? UndecoratedInternalProperty { get; set; } = "undecorated_internal";

        protected string? UndecoratedProtectedProperty { get; set; } = "undecorated_protected";

        private string? UndecoratedPrivateProperty { get; set; } = "undecorated_private";
    }

    internal class ExampleMatchParametersEasyPostObject : EasyPostObject
    {
        public string? Prop1 { get; set; }
    }

    internal class ExampleMatchParameters : Parameters.BaseParameters<ExampleMatchParametersEasyPostObject>
    {
        public string? Prop1 { get; set; }
    }

    internal sealed class ParameterSetWithRequiredAndOptionalParameters : Parameters.BaseParameters<EasyPostObject>
    {
        [TopLevelRequestParameter(Necessity.Required, "test", "required")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? RequiredParameter { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "test", "optional")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? OptionalParameter { get; set; }
    }

    internal sealed class ParameterSetWithCompetingParameters : Parameters.BaseParameters<EasyPostObject>
    {
        [TopLevelRequestParameter(Necessity.Optional, "location")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? AParam { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "location")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? BParam { get; set; }
    }

    internal sealed class ParameterSetWithCompetingParametersNonAlphabetic : Parameters.BaseParameters<EasyPostObject>
    {
        [TopLevelRequestParameter(Necessity.Optional, "location")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? BParam { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "location")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? AParam { get; set; }
    }

    internal sealed class ParameterSetWithOneOrOtherDependentPresenceBasedTopLevelParameters : Parameters.BaseParameters<EasyPostObject>
    {
        [TopLevelRequestParameter(Necessity.Optional, "a_param")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfSet, DependentStatus.MustNotBeSet, "BParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfNotSet, DependentStatus.MustBeSet, "BParam")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? AParam { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "b_param")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfSet, DependentStatus.MustNotBeSet, "AParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfNotSet, DependentStatus.MustBeSet, "AParam")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? BParam { get; set; }
    }

    internal sealed class ParameterSetWithBothOrNeitherDependentPresenceBasedTopLevelParameters : Parameters.BaseParameters<EasyPostObject>
    {
        [TopLevelRequestParameter(Necessity.Optional, "a_param")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfSet, DependentStatus.MustBeSet, "BParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfNotSet, DependentStatus.MustNotBeSet, "BParam")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? AParam { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "b_param")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfSet, DependentStatus.MustBeSet, "AParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfNotSet, DependentStatus.MustNotBeSet, "AParam")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? BParam { get; set; }
    }

    internal sealed class ParameterSetWithOneOrOtherDependentValueBasedTopLevelParameters : Parameters.BaseParameters<EasyPostObject>
    {
        [TopLevelRequestParameter(Necessity.Optional, "a_param")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfValue, "A", DependentStatus.MustBeValue, dependentValue: "B", "BParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfNotValue, "A", DependentStatus.MustNotBeValue, dependentValue: "B", "BParam")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? AParam { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "b_param")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfValue, "B", DependentStatus.MustBeValue, dependentValue: "A", "AParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfNotValue, "B", DependentStatus.MustNotBeValue, dependentValue: "A", "AParam")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? BParam { get; set; }
    }

    internal sealed class ParameterSetWithBothOrNeitherDependentValueBasedTopLevelParameters : Parameters.BaseParameters<EasyPostObject>
    {
        [TopLevelRequestParameter(Necessity.Optional, "a_param")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfValue, "A", DependentStatus.MustNotBeValue, dependentValue: "B", "BParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfNotValue, "A", DependentStatus.MustBeValue, dependentValue: "B", "BParam")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? AParam { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "b_param")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfValue, "B", DependentStatus.MustNotBeValue, dependentValue: "A", "AParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfNotValue, "B", DependentStatus.MustBeValue, dependentValue: "A", "AParam")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? BParam { get; set; }
    }

    internal sealed class ParameterSetWithMixedDependentPresenceAndValueBasedTopLevelParameters : Parameters.BaseParameters<EasyPostObject>
    {
        [TopLevelRequestParameter(Necessity.Optional, "a_param")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfValue, "A", DependentStatus.MustBeSet, "BParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfNotValue, "A", DependentStatus.MustNotBeSet, "BParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfSet, DependentStatus.MustBeValue, dependentValue: "B", "BParam")]
        [TopLevelRequestParameterDependents(IndependentStatus.IfNotSet, DependentStatus.MustNotBeValue, dependentValue: "B", "BParam")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? AParam { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "b_param")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string? BParam { get; set; }
    }

#pragma warning restore CA1852 // Can be sealed

    #endregion
}
