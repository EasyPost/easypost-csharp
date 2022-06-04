// This test checks that EasyPost C# code can be used in F#.
// This test project is running on .NET 6.0, although a success here should mean a success in all versions of .NET.'

namespace EasyPost.Tests.FSharp

open EasyPost.Clients
open Xunit

type FSharpCompileTest() =
    [<Fact>]
    member this.TestCompile() =
        // The assert doesn't really do anything, but as long as this test can run, then the code is compiling correctly.
        let client = new V2Client("")
        let carrierTypesService = client.CarrierTypes
        Assert.NotNull(carrierTypesService)
