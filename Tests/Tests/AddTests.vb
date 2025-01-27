Imports DapperRepoVb.DbItems
Imports Xunit

Namespace Tests

    Public Class AddTests
        Inherits BaseDbTestClass

        <Fact>
        Async Function EnsureWeCanAdd() As Task
            ' how we add
            Dim user = New User With {.Email = "test", .Name = "Mike", .CreatedAt = Date.Now}
            Await Repo.Add(user)

            ' asserts
            Dim result As User = Await GetFirstUser()
            Assert.Equal(user.Email, result.Email)
            Assert.Equal(user.Name, result.Name)
            Assert.Equal(user.CreatedAt, result.CreatedAt)
        End Function
    End Class
End NameSpace