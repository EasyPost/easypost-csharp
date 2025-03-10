// This test checks that EasyPost C# code can be used in NetStandard.

namespace EasyPost.Compatibility.NetStandard

open EasyPost
open Xunit

type NetStandardCompileTest() =
    [<Fact>]
member this.TestCompile() =
        let client = new Client(new ClientConfiguration("fake_api_key"))
        // The assert doesn't really do anything, but as long as this test can run, then the code is compiling correctly.
        Assert.NotNull(client)
