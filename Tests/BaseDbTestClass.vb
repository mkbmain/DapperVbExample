Imports System
Imports Microsoft.Data.Sqlite
Imports System.Data.Common
Imports DapperRepoVb
Imports Xunit
Imports Mkb.DapperRepo.Repo

Namespace Tests
    Public Class BaseDbTestClass
        Protected ReadOnly _dbName As String = Guid.NewGuid.ToString() + ".db"
        Protected ReadOnly Connection As DbConnection = New SqliteConnection("Data Source="+_dbName)
        Protected ReadOnly Repo As SqlRepoAsync = New SqlRepoAsync(Function() Connection)

        Public Sub New()
            CreateDbHelper.CreateDb(_dbName, Connection)
        End Sub
    End Class
End Namespace

