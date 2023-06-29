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
        
        Dim tablesNum = Connection.Query(of String)("SELECT name FROM sqlite_master")
        Assert.True(1 <tablesNum.Count())
        Await Repo.Execute("CREATE TABLE  " + TableName + " (id   integer constraint posts_pk primary key autoincrement)")

        ' asserts
        Dim tables =connection.Query(of String)("SELECT name FROM sqlite_master")
        Assert.True(tables.Count() > tablesNum.Count())
    End Function
End Class