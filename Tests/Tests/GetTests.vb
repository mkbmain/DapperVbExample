Imports DapperRepoVb.DbItems
Imports Mkb.DapperRepo.Search
Imports Tests.Tests
Imports Xunit

Public Class GetTests
    Inherits BaseDbTestClass

    <Fact>
    Async Function EnsureWeCanGet() As Task
        Dim user As User = Await AddUser()
        ' get
        Dim userFromDb = Await Repo.GetById(New User With {.Id = user.Id})

        ' asserts
        Assert.True(UserAreEqual(user, userFromDb))
    End Function


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
    Async Function EnsureWeCanGetAll() As Task
        Dim user As User = Await AddUser()
        Await AddUser()
        Await AddUser()
        Await AddUser()
        ' get
        Dim usersFromDb = Await Repo.GetAll(Of User)

        ' asserts
        Assert.Equal(4, usersFromDb.Count)
    End Function

    <Fact>
    Async Function EnsureWeCanGetAllByX() As Task

        Await AddUser()
        Await AddUser()
        Await AddUser("john")
        Await AddUser()
        ' get
        Dim usersFromDb = Await Repo.GetAllByX(Of User, String)(NameOf(User.Name), "john")

        ' asserts
        Assert.Equal(1, usersFromDb.Count)
    End Function

    <Fact>
    Async Function EnsureWeCanSearch() As Task

        Await AddUser()
        Await AddUser()
        Dim user As User = Await AddUser("john")
        Await AddUser()
        ' get
        Dim usersFromDb = Await Repo.Search(Of User)(New User With {.CreatedAt = user.CreatedAt.AddDays(1), .Email = user.Email},
{SearchCriteria.Create(NameOf(user.Email), SearchType.Equals), SearchCriteria.Create(NameOf(user.CreatedAt), SearchType.LessThan)})

        ' asserts
        Assert.Equal(1, usersFromDb.Count)
        Assert.True(UserAreEqual(user, usersFromDb.First()))
    End Function
End Class