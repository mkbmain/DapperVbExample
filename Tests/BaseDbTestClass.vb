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

        Public Async Function AddUser(Optional name As String = Nothing) As Task(Of User)
            If name = Nothing Then
                name = "mike"
            End If
            Await _
     Connection.ExecuteAsync(
         New CommandDefinition($"insert into users (Name,CreatedAt,Email) values ('{name}','2020-01-01','{Guid.NewGuid()}')"))
            Dim result As User = Await GetFirstUser()
            Assert.NotNull(result)
            Return result
        End Function

        Public Shared Function UserAreEqual(user As User, user2 As User) As Boolean
            Return user.Id = user2.Id AndAlso user.CreatedAt = user2.CreatedAt AndAlso user.Email = user.Email
        End Function
    End Class
End Namespace

