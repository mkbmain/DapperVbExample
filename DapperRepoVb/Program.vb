Imports System.Data.Common
Imports DapperRepoVb.DbItems
Imports Microsoft.Data.Sqlite
Imports Mkb.DapperRepo.Repo
Imports Mkb.DapperRepo.Search



Module Program
    Private Const SqlFile As String = "OurDbFile.sqlite"
    Private ReadOnly GetNewSqlConnection As Func(Of DbConnection) = Function() New SqliteConnection($"Data Source={SqlFile}")
    Private ReadOnly Connection As DbConnection = GetNewSqlConnection()
    ' you might need to change this depending on where your sql instance is and if its windows auth

    Public Sub Main()
        CreateDbHelper.CreateDb(SqlFile, Connection)
        Task.Run(AddressOf AsyncRun).GetAwaiter().GetResult()
    End Sub


    Private Async Function AsyncRun() As Task ' async

        Dim repo = New SqlRepoAsync(Function() Connection) ' use it as singleton (single instance used for every Connection)
        '''     Dim repo = New SqlRepoAsync(GetNewSqlConnection) ' use new dbconnection instance for every call
        Await ClearDbAsync(repo)



        ' we add a user
        ' lets add some users
        Dim user As User = New User With {.CreatedAt = Date.Now, .Email = "test@email.com", .Name = "Michael"}

        Await repo.Add(user)
        Await repo.Add(New User With {.CreatedAt = Date.Now, .Email = "test2@email.com", .Name = ""})
        Await repo.Add(New User With {.CreatedAt = Date.Now.AddDays(-1), .Email = "test3@email.com", .Name = "John"})
        Await repo.Add(New User With {.CreatedAt = Date.Now.AddDays(3), .Email = "test4@email.com", .Name = "Jane"})
        Await repo.Add(New User With {.CreatedAt = Date.Now.AddDays(-1), .Email = "test4@email.com", .Name = "Jane"})
        ' we add a post
        Dim users = Await repo.GetExactMatches(user, False)

        Enumerable.Range(0, 22).Select(Function(e)
                                           repo.Add(New Post With {.PostedAt = Date.Now, .UserId = users.First().Id, .Text = $"Test{e}Post"})
                                           Return e
                                       End Function).ToArray()

        ' we search for 11 post
        Dim elven = (Await repo.Search(Of Post)(NameOf(Post.Text), "%11%")).FirstOrDefault()
        If elven Is Nothing Then
            Throw New Exception("Opps")
        End If
        ' we edit the post
        elven.Text = "Edited"
        Await repo.Update(elven)

        ' we get all posts again from db
        Dim allPosts = Await repo.GetAll(Of Post)

        ' get by name this is a quick exact match on 1 type
        Dim getByName = Await repo.GetAllByX(Of User, String)(NameOf(user.Name), "John")

        ' more complex search with multiple things to check
        Dim search = Await repo.Search(New User With {.Name = "Jane", .CreatedAt = Date.Now},
                                       {New SearchCriteria _
                                          With {.PropertyName = NameOf(user.Name), .SearchType = SearchType.Equals},
                                        New SearchCriteria _
                                          With {.PropertyName = NameOf(user.CreatedAt), .SearchType = SearchType.GreaterThan}})


        ' we check edit happened
        If allPosts.Any(Function(t) t.Text = "Edited") = False Then
            Throw New Exception("Opps")
        End If

        Await ClearDbAsync(repo)
    End Function
End Module


