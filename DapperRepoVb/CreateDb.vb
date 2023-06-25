Imports System.Data.Common
Imports System.IO
Imports Dapper

Public Class CreateDbHelper
    Public Shared Sub CreateDb(sqlFile As String, connection As DbConnection)
        If File.Exists(sqlFile) Then

            File.Delete(sqlFile)
        End If
        File.WriteAllBytes(sqlFile, Array.Empty(Of Byte))
        Dim setup As String = File.ReadAllText("SqliteToSetUpDb.txt")
        connection.Execute(setup)
    End Sub
End Class
