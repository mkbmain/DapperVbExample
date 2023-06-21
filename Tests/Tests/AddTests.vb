Imports System
Imports Dapper
Imports DapperRepoVb.DbItems
Imports Xunit

Namespace Tests
    Public Class AddTests
        Inherits BaseDbTestClass

        <Fact>
        Async Function EnsureWeCanAdd() As Task
            dim user = new User With{.Email="test",.Name="Mike",.CreatedAt= DateTime.Now}
            Await Repo.Add(user)

            dim result as User =
                    Await Connection.QueryFirstAsync (of User)(new CommandDefinition("select * from users"))
            Assert.Equal(user.Email, result.Email)
            Assert.Equal(user.Name, result.Name)
            Assert.Equal(user.CreatedAt, result.CreatedAt)
        End Function
    End Class
End Namespace

