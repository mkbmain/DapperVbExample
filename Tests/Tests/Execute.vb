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

        ' execute
        Dim tablesNum = GetTables(Connection)
        Assert.True(1 < tablesNum)
        Await _
            Repo.Execute("CREATE TABLE  " + TableName + " (id   integer constraint posts_pk primary key autoincrement)")

        ' asserts
        Dim newTables = GetTables(Connection)
        Assert.True(newTables > tablesNum)
    End Function

    Private Function GetTables(connection As DbConnection) As Integer
        Return connection.ExecuteScalar(Of Integer)("SELECT count(name) FROM sqlite_master")
    End Function
End Class