Imports DapperRepoVb.DbItems
Imports Tests.Tests
Imports Xunit
Imports Dapper

Public Class AddTests
    Inherits BaseDbTestClass

    <Fact>
    Async Function EnsureWeCanAdd() As Task
        ' how we add
        Dim user = New User With {.Email = "test", .Name = "Mike", .CreatedAt = DateTime.Now}
        Await Repo.Add(user)

        ' asserts
        Dim result As User = Await Connection.QueryFirstAsync(Of User)(New CommandDefinition("select * from users"))
        Assert.Equal(user.Email, result.Email)
        Assert.Equal(user.Name, result.Name)
        Assert.Equal(user.CreatedAt, result.CreatedAt)
    End Function
End Class

