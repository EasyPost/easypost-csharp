using System;
using System.Collections.Generic;
using EasyPost.Exceptions.General;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal;
using Newtonsoft.Json;
using Xunit;
using CustomAssertions = EasyPost.Tests._Utilities.Assertions.Assert;

namespace EasyPost.Tests.UtilitiesTests
{
    public class JsonSerializationTest
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestConvertJsonToDictionary()
        {
            try
            {
                JsonSerialization.ConvertJsonToObject<Dictionary<string, object>>(Json);
            }
            catch (Exception)
            {
                Assert.Fail("Failed to deserialize JSON");
            }

            // bad JSON data should throw an exception
            Assert.Throws<JsonDeserializationError>(() => JsonSerialization.ConvertJsonToObject<Dictionary<string, object>>("bad_json_data"));

            // missing data should throw an exception
            Assert.Throws<JsonNoDataError>(() => JsonSerialization.ConvertJsonToObject<Dictionary<string, object>>(data: null));
        }

        [Fact]
        [Testing.Function]
        public void TestConvertJsonToExpandoObject()
        {
            try
            {
                JsonSerialization.ConvertJsonToObject(Json);
            }
            catch (Exception)
            {
                Assert.Fail("Failed to deserialize JSON");
            }

            // bad JSON data should throw an exception
            Assert.Throws<JsonDeserializationError>(() => JsonSerialization.ConvertJsonToObject("bad_json_data"));

            // missing data should throw an exception
            Assert.Throws<JsonNoDataError>(() => JsonSerialization.ConvertJsonToObject<JsonObjectDetails>(data: null));
        }

        [Fact]
        [Testing.Function]
        public void TestConvertJsonToObject()
        {
            try
            {
                JsonSerialization.ConvertJsonToObject<JsonObjectRoot>(Json);
            }
            catch (Exception)
            {
                Assert.Fail("Failed to deserialize JSON");
            }

            // bad JSON data should throw an exception
            Assert.Throws<JsonDeserializationError>(() => JsonSerialization.ConvertJsonToObject<JsonObjectRoot>("bad_json_data"));

            // missing data should throw an exception
            Assert.Throws<JsonNoDataError>(() => JsonSerialization.ConvertJsonToObject<JsonObjectRoot>(data: null));
        }

        [Fact]
        [Testing.Function]
        public void TestConvertObjectToJson()
        {
            try
            {
                JsonObjectRoot root = new()
                {
                    Id = "id",
                    Details = new JsonObjectDetails
                    {
                        Name = "details_name",
                        Value = 100
                    }
                };

                JsonSerialization.ConvertObjectToJson(root);
                CustomAssertions.Pass(); // we got this far with no exceptions, so we're good
            }
            catch (Exception)
            {
                Assert.Fail("Failed to serialize JSON");
            }

            // bad object should throw an exception (in theory, can't actually trigger this, Newtonsoft.Json is too smart)
        }

        [Fact]
        [Testing.Parameters]
        public void TestConvertJsonToObjectWithRootElements()
        {
            try
            {
                JsonSerialization.ConvertJsonToObject<JsonObjectDetails>(Json, rootElementKeys: new List<string> { "details" });
            }
            catch (Exception)
            {
                Assert.Fail("Failed to deserialize JSON");
            }

            // missing data should throw an exception
            Assert.Throws<JsonNoDataError>(() => JsonSerialization.ConvertJsonToObject<JsonObjectDetails>(data: null, rootElementKeys: new List<string> { "details" }));

            // bad keys should throw an exception
            Assert.Throws<JsonNoDataError>(() => JsonSerialization.ConvertJsonToObject<JsonObjectDetails>(data: null, rootElementKeys: new List<string> { "nonexistent_key" }));
        }

        #endregion

        private const string Json = "{\"id\":\"root_id\",\"details\":{\"value\":100,\"name\":\"details_name\"}}";

        public class JsonObjectRoot
        {
            #region JSON Properties

            [JsonProperty("details")]
            public JsonObjectDetails? Details { get; set; }
            [JsonProperty("id")]
            public string? Id { get; set; }

            #endregion
        }

        public class JsonObjectDetails
        {
            #region JSON Properties

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("value")]
            public int? Value { get; set; }

            #endregion
        }
    }
}
