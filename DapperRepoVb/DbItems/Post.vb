' db models here
Imports Mkb.DapperRepo.Attributes

Namespace DbItems
    <SqlTableName("Posts")>
    Public Class Post
        <PrimaryKey()>
        Public Property Id() As Integer?
        <SqlColumnName("user_id")>
        Public Property UserId() As Integer
        Public Property Text() As String
        Public Property PostedAt() As Date
    End Class
End NameSpace