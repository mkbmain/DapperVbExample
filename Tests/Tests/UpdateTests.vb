Imports DapperRepoVb.DbItems
Imports Xunit

Namespace Tests
    Public Class UpdateTests
        Inherits BaseDbTestClass

        <Fact>
        Async Function EnsureWeCanUpdate() As Task
            Dim name As String = "Mike"

            Dim user As User = Await AddUser()
            Assert.NotEqual(name, user.Name)
            ' update
            user.Name = name
            Await Repo.Update(user)

            ' asserts
            user = Await GetFirstUser()
            Assert.Equal(name, user.Name)
        End Function
    End Class
End NameSpace