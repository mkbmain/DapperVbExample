Imports Mkb.DapperRepo.Attributes

Namespace DbItems
    <SqlTableName("Users")>                             ' for pluralization but can also be used for old schema names i.e USER_TABLE is a horrible class name but may be a legacy table name
    Public Class User
         <PrimaryKey()>                                  ' used to denote this is primary key for lookups updates etc
         Public Property Id() As Nullable(Of Guid)       ' marked as nullable as db auto insert so we don't need to repo won't insert null fields  will return it back on add
         Public Property Name() As String
         Public Property Email() As String
         Public Property CreatedAt() As DateTime
    End Class
End NameSpace