'This test checks that EasyPost C# code can be used in Visual Basic.
'This test project is running on .NET 6.0, although a success here should mean a success in all versions of .NET.
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class VbCompileTest
    <TestMethod>
    Public Sub TestCompile()
        Dim address = New Address()

        'Do not need to actually assert anything here. If it runs, it means it compiled successfully.
        Assert.IsNotNull(address)
    End Sub
End Class
