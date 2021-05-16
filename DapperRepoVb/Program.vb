Imports DapperRepoVb.DbItems
Imports Mkb.DapperRepo.Repo


module Program
    Const Connection As String = "Server=192.168.0.204;Database=DapperTestDb;User Id=sa;Password=12345678;" ' you might need to change this depending on where your sql instance is and if its windows auth

    Public Sub Main()
        Task.Run(AddressOf AsyncRun).Wait() ' this is in every example but the .wait locks don't it ???? surely this is bad 
        Run()
    End Sub

    Private Sub Run() ' sync
        Dim repo = New SqlRepo(Connection)
        ClearDb(repo)


        ' we add a user
        Dim user = repo.Add(New User With {.CreatedAt = DateTime.Now, .Email = "test@email.com", .Name = "Michael"})
        repo.Add(New User With {.CreatedAt = DateTime.Now, .Email = "test2@email.com", .Name = ""})
        repo.Add(New User With {.CreatedAt = DateTime.Now, .Email = "test3@email.com", .Name = "John"})
        repo.Add(New User With {.CreatedAt = DateTime.Now, .Email = "test4@email.com", .Name = "Jane"})
        ' we add a post
        For i = 0 To 22
            repo.Add(New Post With {.PostedAt = DateTime.Now, .UserId = user.Id, .Text = $"Test{i}Post"})
        Next

        ' we search for 11 post
        Dim elven = (repo.Search(Of Post)(NameOf(Post.Text), "%11%")).ToArray()
        If elven.Count() <> 1 Then
            Throw New Exception("Opps")
        End If
        ' we edit the post
        elven.First().Text = "Edited"
        repo.Update(elven.First())

        ' we get all posts again from db
        Dim allPosts = repo.GetAll(Of Post)

        ' we check edit happened
        If (allPosts.Any(Function(t) t.Text = "Edited") = False) Then
            Throw New Exception("Opps")
        End If

        ClearDb(repo)
    End Sub

    Private Async Function AsyncRun() As Task ' async

        Dim repo = New SqlRepoAsync(Connection)
        Await ClearDbAsync(repo)


        ' we add a user
        Dim user = Await repo.Add(New User With {.CreatedAt = DateTime.Now, .Email = "test@email.com", .Name = "Michael"})
        Await repo.Add(New User With {.CreatedAt = DateTime.Now, .Email = "test2@email.com", .Name = ""})
        Await repo.Add(New User With {.CreatedAt = DateTime.Now, .Email = "test3@email.com", .Name = "John"})
        Await repo.Add(New User With {.CreatedAt = DateTime.Now, .Email = "test4@email.com", .Name = "Jane"})
        ' we add a post
        For i = 0 To 22
            Await repo.Add(New Post With {.PostedAt = DateTime.Now, .UserId = user.Id, .Text = $"Test{i}Post"})
        Next

        ' we search for 11 post
        Dim elven = (Await repo.Search(Of Post)(NameOf(Post.Text), "%11%")).ToArray()
        If elven.Count() <> 1 Then
            Throw New Exception("Opps")
        End If
        ' we edit the post
        elven.First().Text = "Edited"
        Await repo.Update(elven.First())

        ' we get all posts again from db
        Dim allPosts = Await repo.GetAll(Of Post)

        ' we check edit happened
        If (allPosts.Any(Function(t) t.Text = "Edited") = False) Then
            Throw New Exception("Opps")
        End If

        Await ClearDbAsync(repo)
    End Function
End Module


