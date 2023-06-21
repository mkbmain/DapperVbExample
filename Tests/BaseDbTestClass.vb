Imports System
Imports Microsoft.Data.Sqlite
Imports System.Data.Common
Imports DapperRepoVb
Imports Xunit
Imports Mkb.DapperRepo.Repo
Imports Dapper
Imports DapperRepoVb.DbItems

Namespace Tests
    Public Class BaseDbTestClass
        Protected ReadOnly _dbName As String = Guid.NewGuid.ToString() + ".db"
        Protected ReadOnly Connection As DbConnection = New SqliteConnection("Data Source=" + _dbName)
        Protected ReadOnly Repo As SqlRepoAsync = New SqlRepoAsync(Function() Connection)

        Public Sub New()
            CreateDbHelper.CreateDb(_dbName, Connection)
        End Sub

        Public Async Function GetFirstUser() As Task(Of User)
            Return Await Connection.QueryFirstOrDefaultAsync(Of User)(New CommandDefinition("select * from users"))
        End Function

        Public Async Function AddUser() As Task(Of User)
            Await _
     Connection.ExecuteAsync(
         New CommandDefinition($"insert into users (Name,CreatedAt,Email) values ('mike','2020-01-01','test')"))
            Dim result As User = Await GetFirstUser()
            Assert.NotNull(result)
            Return result
        End Function
    End Class
End Namespace

