Imports DapperRepoVb.DbItems
Imports Mkb.DapperRepo.Search
Imports Tests.Tests
Imports Xunit

Public Class CountTests
    Inherits BaseDbTestClass

    <Fact>
    Async Function EnsureWeCount() As Task
        Dim user As User = Await AddUser()
        Await AddUser()
        Await AddUser()
        Await AddUser()
        ' get
        Dim usersFromDb = Await Repo.Count(Of User)

        ' asserts
        Assert.Equal(4, usersFromDb)
    End Function

    <Fact>
    Async Function EnsureWeCanSearchCountSingleCriteria() As Task

        Await AddUser()
        Await AddUser()
        Await AddUser("john")
        Await AddUser()
        ' get
        Dim usersFromDb = Await Repo.SearchCount(New User With {.Name = "john"}, SearchCriteria.Create(NameOf(User.Name), SearchType.Equals))

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
        Dim usersFromDb = Await Repo.SearchCount(New User With {.CreatedAt = user.CreatedAt.AddDays(1), .Email = user.Email},
{SearchCriteria.Create(NameOf(user.Email), SearchType.Equals), SearchCriteria.Create(NameOf(user.CreatedAt), SearchType.LessThan)})

        ' asserts
        Assert.Equal(1, usersFromDb)
    End Function
End Class