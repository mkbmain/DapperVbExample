' db models here
Imports Mkb.DapperRepo.Attributes

Namespace DbItems
    <SqlTableName("Posts")>
    Public Class Post
        <PrimaryKey()>
        Public Property Id() As Nullable(Of Int32)
        Public Property UserId() As Integer
        Public Property Text() As String
        Public Property PostedAt() As DateTime
    End Class
End NameSpace