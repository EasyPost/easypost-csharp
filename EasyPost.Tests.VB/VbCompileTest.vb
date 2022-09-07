'This test checks that EasyPost C# code can be used in Visual Basic.
'This test project is running on .NET 6.0, although a success here should mean a success in all versions of .NET.
Imports EasyPost.Models.API
Imports Xunit

Public Class VbCompileTest
    <Fact>
    Public Sub TestCompile()
        Dim address = New Address()

        'Do not need to actually assert anything here. If it runs, it means it compiled successfully.
        Assert.NotNull(address)
    End Sub
End Class
