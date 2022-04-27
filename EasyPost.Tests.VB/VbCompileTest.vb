'This test checks that EasyPost C# code can be used in Visual Basic.
'This test project is running on .NET 6.0, although a success here should mean a success in all versions of .NET.
Imports EasyPost.Clients
Imports EasyPost.Models
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class VbCompileTest
    <TestMethod>
    Public Sub TestCompile()
        'MSTest does not seem to support asynchronous tests in VB, so we can't attempt any API calls.'
        'We'll work off the assumption that if this code compiles, then we have VB compatibility.'
        Dim client = New V2Client(apiKey:="")
        Dim carrierTypesService = client.CarrierTypes
        Assert.IsNotNull(carrierTypesService)
        Assert.IsInstanceOfType(carrierTypesService.All(), GetType(Task(Of List(Of CarrierType))))
    End Sub
End Class
