'This test checks that EasyPost C# code can be used in Visual Basic.
'This test project is running on .NET 6.0, although a success here should mean a success in all versions of .NET.
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class VbCompileTest
    <TestMethod>
    Public Sub TestCompile()
        'API key is not set, so calling CarrierType.All() will produce an error. But if this runs, then the code compiled properly.
        Assert.ThrowsException(Of ClientNotConfigured)(Function() CarrierType.All())
    End Sub

    <TestMethod>
    Public Sub TestAddress()
        Dim addressData As New Dictionary(Of String, Object)()

        addressData.Add("name", "John Smith")
        addressData.Add("street1", "123 Main St")
        addressData.Add("city", "San Francisco")
        addressData.Add("state", "CA")
        addressData.Add("zip", "94107")
        addressData.Add("country", "US")

        'Without an API key, this will throw an error. But as long as it's a ClientNotConfigured exception, it's a success.
        Assert.ThrowsException(Of ClientNotConfigured)(Function() Address.Create(addressData))
    End Sub
End Class
