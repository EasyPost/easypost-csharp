using System.Collections.Generic;
using System.ComponentModel;
using EasyPost._base;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal;
using Newtonsoft.Json;
using Xunit;

// ReSharper disable InconsistentNaming

// ReSharper disable EqualExpressionComparison

namespace EasyPost.Tests.UtilitiesTests
{
    public class EnumTest
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestAll()
        {
            // get a list of all the enum values
            List<string> enumValues = new()
            {
                "A",
                "B",
                "C"
            };
            IEnumerable<TestValueEnum> enums = Enum.GetAll<TestValueEnum>();
            foreach (TestValueEnum? @enum in enums)
            {
                Assert.IsType<TestValueEnum>(@enum);
                Assert.Contains(enumValues, x => x == (string)@enum.Value);
            }
        }

        [Fact]
        [Testing.Function]
        public void TestEquals()
        {
            // compare against same value
            Assert.True(TestValueEnum.A.Equals(TestValueEnum.A));
            Assert.True(TestValueEnum.A == TestValueEnum.A);
            Assert.False(TestValueEnum.A != TestValueEnum.A);
            // compareTo checks if x comes before y
            // since they are the same, it should return 0
            Assert.True(TestValueEnum.A.CompareTo(TestValueEnum.A) == 0);

            // compare against different value
            Assert.False(TestValueEnum.A.Equals(TestValueEnum.B));
            Assert.False(TestValueEnum.A == TestValueEnum.B);
            Assert.True(TestValueEnum.A != TestValueEnum.B);
            // compareTo checks if x comes before y
            // should return -1
            Assert.True(TestValueEnum.A.CompareTo(TestValueEnum.B) == -1);

            // compare against null
            Assert.False(TestValueEnum.A.Equals(null));
            Assert.False(TestValueEnum.A == null);
            Assert.True(TestValueEnum.A != null);
        }

        [Fact]
        [Testing.Function]
        public void TestGreaterThan()
        {
            Assert.True(TestValueEnum.B > TestValueEnum.A);
            Assert.False(TestValueEnum.B > TestValueEnum.B);
        }

        [Fact]
        [Testing.Function]
        public void TestGreaterThanOrEqual()
        {
            Assert.True(TestValueEnum.B >= TestValueEnum.A);
            Assert.True(TestValueEnum.B >= TestValueEnum.B);
        }

        [Fact]
        [Testing.Function]
        public void TestLessThan()
        {
            Assert.True(TestValueEnum.A < TestValueEnum.B);
            Assert.False(TestValueEnum.A < TestValueEnum.A);
        }

        [Fact]
        [Testing.Function]
        public void TestLessThanOrEqual()
        {
            Assert.True(TestValueEnum.A <= TestValueEnum.B);
            Assert.True(TestValueEnum.A <= TestValueEnum.A);
        }

        [Fact]
        [Testing.Function]
        public void TestToString()
        {
            // normal enums print their ID as a string
            Assert.Equal("1", TestEnum.A.ToString());

            // value enums print their value as a string
            Assert.Equal("A", TestValueEnum.A.ToString());
        }

        [Fact]
        [Testing.Function]
        public void TestSerialization()
        {
            var obj = new TestClass
            {
                Enum = TestEnum.A,
                ValueEnum = TestValueEnum.A,
            };

            // run JSON serialization process
            var dictionary = obj.AsDictionary();

            // check that the enum values are serialized correctly
            Assert.Equal(1, (int)(long)dictionary["enum"]); // need to cast to avoid int64 vs int32 issues (https://stackoverflow.com/a/31737978)
            Assert.Equal("A", dictionary["value_enum"]);
        }

        [Fact]
        [Testing.Function]
        public void TestDeserialization()
        {
            var dictionary = new Dictionary<string, object>
            {
                { "enum", 1 },
                { "value_enum", "A" },
            };

            // run JSON deserialization process
            var obj = JsonConvert.DeserializeObject<TestClass>(JsonConvert.SerializeObject(dictionary));

            // check that the enum values are deserialized correctly
            Assert.Equal(TestEnum.A, obj.Enum);
            Assert.Equal(TestValueEnum.A, obj.ValueEnum);
        }

        #endregion

        // decorated with JsonConverter attribute so that it can be de/serialized custom
        [JsonConverter(typeof(EnumJsonConverter<TestEnum>))]
        internal sealed class TestEnum : Enum
        {
            public static readonly TestEnum A = new(1);
            public static readonly TestEnum B = new(2);
            public static readonly TestEnum C = new(3);

            private TestEnum(int id) : base(id)
            {
            }
        }

        // decorated with JsonConverter attribute so that it can be de/serialized custom
        [JsonConverter(typeof(ValueEnumJsonConverter<TestValueEnum>))]
        internal sealed class TestValueEnum : ValueEnum
        {
            public static readonly TestValueEnum A = new(1, "A");
            public static readonly TestValueEnum B = new(2, "B");
            public static readonly TestValueEnum C = new(3, "C");

            private TestValueEnum(int id, object value) : base(id, value)
            {
            }
        }

        internal sealed class TestClass : EasyPostObject
        {
            [JsonProperty("enum")]
            public TestEnum? Enum { get; set; }

            [JsonProperty("value_enum")]
            public TestValueEnum? ValueEnum { get; set; }
        }
    }
}
