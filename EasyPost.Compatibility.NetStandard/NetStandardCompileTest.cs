// This test checks that EasyPost C# code can be used in NetStandard.

using EasyPost;
using Xunit;

namespace EasyPost.Compatibility.NetStandard
{
    public class NetStandardCompileTest
    {
        [Fact]
        public void TestCompile()
        {
            var client = new Client(new ClientConfiguration("fake_api_key"));
            // The assert doesn't really do anything, but as long as this test can run, then the code is compiling correctly.
            Assert.NotNull(client);
        }
    }
}
