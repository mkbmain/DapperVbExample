Imports DapperRepoVb.DbItems
Imports Mkb.DapperRepo.Search
Imports Xunit

Namespace Tests
    Public Class CountTests
        Inherits BaseDbTestClass

        <Fact>
        Async Function EnsureWeCount() As Task
            Const amount as integer = 4
            dim tasks = Enumerable.Range(0, amount).Select(function(i) AddUser())
            Await Task.WhenAll(tasks)
            ' get
            Dim usersFromDb = Await Repo.Count (Of User)

            ' asserts
            Assert.Equal(amount, usersFromDb)
        End Function

        <Fact>
        Async Function EnsureWeCanSearchCountSingleCriteria() As Task
            
            Await AddUser()
            Await AddUser("john")
            Await AddUser()
            ' get
            Dim usersFromDb =
                    Await _
                    Repo.SearchCount(New User With {.Name = "john"},
                                     SearchCriteria.Create(NameOf(User.Name), SearchType.Equals))

            ' asserts
            Assert.Equal(1, usersFromDb)
        End Function


        <Fact>
        Async Function EnsureWeCanSearchCount() As Task

            Await AddUser()
            Await AddUser()
            Dim user As User = Await AddUser("john")
            Await AddUser()
            ' get
            Dim usersFromDb =
                    Await Repo.SearchCount(New User With {.CreatedAt = user.CreatedAt.AddDays(1), .Email = user.Email},
                                           {SearchCriteria.Create(NameOf(user.Email), SearchType.Equals),
                                            SearchCriteria.Create(NameOf(user.CreatedAt), SearchType.LessThan)})

            ' asserts
            Assert.Equal(1, usersFromDb)
        End Function
    End Class
End NameSpace