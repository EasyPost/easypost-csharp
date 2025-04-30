'This test checks that EasyPost C# code can be used in Visual Basic.
Imports Xunit

Public Class VbCompileTest
    <Fact>
    Public Sub TestCompile()
        Dim client = New Client(New ClientConfiguration("fake_api_key"))

        'Do not need to actually assert anything here. If it runs, it means it compiled successfully.
        Assert.NotNull(client)
    End Sub
End Class
