' This test checks that EasyPost C# code can be used in Visual Basic.
' This test project is running on .NET 6.0, although a success here should mean a success in all versions of .NET.
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class VbCompileTest
    <TestMethod>
    Public Sub Test()
        ' API key is not set, so calling CarrierType.All() will produce an error.
        ' But if this runs, then the code compiled properly.
        Assert.ThrowsException(Of ClientNotConfigured)(Function() CarrierType.All())
    End Sub
End Class
