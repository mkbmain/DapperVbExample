Imports System.Data.Common
Imports Dapper
Imports Tests.Tests
Imports Xunit

Public Class ExecuteTests
    Inherits BaseDbTestClass

    <Fact>
    Async Function EnsureWeCanExecute() As Task
        Await AddUser()
        Const TableName = "boohoo"
        Dim User = Await GetFirstUser()
        Assert.NotNull(User)
        ' delete

        Dim tablesNum = GetTables(Connection).Count()
        Assert.True(1 < tablesNum)
        Await _
            Repo.Execute("CREATE TABLE  " + TableName + " (id   integer constraint posts_pk primary key autoincrement)")

        ' asserts
        dim newTables = GetTables(Connection).Count()
        Assert.True(newTables > tablesNum)
    End Function

    Private Function GetTables(connection as DbConnection) As IEnumerable(Of String)
        Return connection.Query (of String)("SELECT name FROM sqlite_master")
    End Function
End Class