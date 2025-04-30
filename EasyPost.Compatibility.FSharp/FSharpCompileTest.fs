// This test checks that EasyPost C# code can be used in F#.

namespace EasyPost.Compatibility.FSharp

open EasyPost
open Xunit

type FSharpCompileTest() =
    [<Fact>]
    member this.TestCompile() =
        let client = new Client(new ClientConfiguration("fake_api_key"))
        // The assert doesn't really do anything, but as long as this test can run, then the code is compiling correctly.
        Assert.NotNull(client)
