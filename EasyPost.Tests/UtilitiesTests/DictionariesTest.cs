using System.Collections.Generic;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Extensions;
using Xunit;

namespace EasyPost.Tests.UtilitiesTests
{
    public class DictionariesTest
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestConvertNonNullDictionaryToNullableDictionary()
        {
            Dictionary<string, object> nonNullableDictionary = GetNonNullableDictionary();
            Dictionary<string, object?> nullableDictionary = nonNullableDictionary.ToStringNullableObjectDictionary();
            Assert.True(nullableDictionary.ContainsKey(Key)); // string value was included during conversion
            Assert.NotNull(nullableDictionary[Key]);
        }

        [Fact]
        [Testing.Function]
        public void TestConvertNullableDictionaryToNonNullDictionary()
        {
            Dictionary<string, object?> nullableDictionary = GetNullableDictionary();
            Dictionary<string, object> nonNullableDictionary = nullableDictionary.ToStringNonNullableObjectDictionary();
            Assert.False(nonNullableDictionary.ContainsKey(Key)); // null value was not included during conversion
        }

        #endregion

        private const string Key = "key";

        private static readonly object Value = "value";

        private static Dictionary<string, object> GetNonNullableDictionary()
        {
            return new Dictionary<string, object>
            {
                { Key, Value },
            };
        }

        private static Dictionary<string, object?> GetNullableDictionary()
        {
            return new Dictionary<string, object?>
            {
                { Key, null },
            };
        }
    }
}
