'This test checks that EasyPost C# code can be used in Visual Basic.
'This test project is running on .NET 6.0, although a success here should mean a success in all versions of .NET.
Imports EasyPost.Clients
Imports EasyPost.Models.V2
Imports Xunit

Public Class VbCompileTest
    <Fact>
    Public Sub TestCompile()
        'MSTest does not seem to support asynchronous tests in VB, so we can't attempt any API calls.'
        'We'll work off the assumption that if this code compiles, then we have VB compatibility.'
        Dim client = New Client(apiKey:="", apiVersion := ApiVersion.Latest)
        Dim carrierTypesService = client.CarrierTypes
        Assert.NotNull(carrierTypesService)
        Assert.IsType(GetType(Task(Of List(Of CarrierType))), carrierTypesService.All())
    End Sub
End Class
