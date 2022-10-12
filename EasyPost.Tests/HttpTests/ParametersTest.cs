using System.Collections.Generic;
using EasyPost.Http;
using EasyPost.Tests._Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.HttpTests
{
    public class ParametersTest
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestWrapParametersDictionary()
        {
            Dictionary<string, object> dictionaryToWrap = new()
            {
                { "foo", "bar" },
                { "baz", "qux" }
            };

            Dictionary<string, object> wrappedDictionary = dictionaryToWrap.Wrap("top", "middle", "bottom");

            Dictionary<string, object> expectedDictionary = new()
            {
                {
                    "top",
                    new Dictionary<string, object>
                    {
                        {
                            "middle", new Dictionary<string, object>
                            {
                                {
                                    "bottom", new Dictionary<string, object>
                                    {
                                        { "foo", "bar" },
                                        { "baz", "qux" }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            Assert.Equal(expectedDictionary, wrappedDictionary);
        }

        [Fact]
        [Testing.Function]
        public void TestWrapParametersList()
        {
            List<string> listToWrap = new()
            {
                "foo",
                "bar"
            };

            Dictionary<string, object> wrappedDictionary = listToWrap.Wrap("top", "middle", "bottom");

            Dictionary<string, object> expectedDictionary = new()
            {
                {
                    "top",
                    new Dictionary<string, object>
                    {
                        {
                            "middle", new Dictionary<string, object>
                            {
                                {
                                    "bottom", new List<string>
                                    {
                                        "foo",
                                        "bar"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            Assert.Equal(expectedDictionary, wrappedDictionary);
        }

        #endregion
    }
}
