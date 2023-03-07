using System.Collections.Generic;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Extensions;
using Xunit;

namespace EasyPost.Tests.UtilitiesTests
{
    public class EnumerablesTest
    {
        #region Tests

        [Fact]
        [Testing.Function]
        public void TestEach()
        {
            List<int> values = new() { 1, 2, 3, 4, 5 };

            List<int> expected = new() { 2, 3, 4, 5, 6 };

            List<int> actual = new();

            values.Each((index, value) => actual.Add(value + 1));

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
