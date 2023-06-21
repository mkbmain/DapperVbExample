Imports DapperRepoVb.DbItems
Imports Tests.Tests
Imports Xunit
Imports Dapper

Public Class UpdateTests
    Inherits BaseDbTestClass

    <Fact>
    Async Function EnsureWeCanUpdate() As Task
        Dim name As String = "Mike"
        Await _
            Connection.ExecuteAsync(
                New CommandDefinition($"insert into users (Name,CreatedAt,Email) values ('{name}','2020-01-01','test')"))
        Dim result As User = Await Connection.QueryFirstAsync(Of User)(New CommandDefinition("select * from users"))
        Assert.NotNull(result)
        Assert.Equal(name, result.Name)


        ' update
        result.Name = name + "2"
        Await Repo.Update(result)

        ' asserts
        result = Await Connection.QueryFirstAsync(Of User)(New CommandDefinition("select * from users"))
        Assert.Equal(name + "2", result.Name)
    End Function
End Class
