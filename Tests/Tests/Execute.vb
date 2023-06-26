Imports Dapper
Imports Tests.Tests
Imports Xunit

Public Class ExecuteTests
	Inherits BaseDbTestClass

	<Fact>
	Async Function EnsureWeCanDelete() As Task
		Await AddUser()
		Const TableName = "boohoo"
		Dim User = Await GetFirstUser()
		Assert.NotNull(User)
		' delete


		Dim tables = Connection.Execute("SELECT name FROM sqlite_master  WHERE type='" + TableName + "'")
		Assert.Equal(-1, tables)
		Await Repo.Execute("CREATE TABLE  " + TableName + " (id   integer constraint posts_pk primary key autoincrement)")

		' asserts
		tables = Connection.Execute("SELECT name FROM sqlite_master  WHERE type='" + TableName + "'")
	'	Assert.Equal(1, tables)
	End Function
End Class