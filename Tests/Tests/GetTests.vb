Imports DapperRepoVb.DbItems
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
        UserAreEqual(user, userFromDb)
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
    Async Function EnsureWeCanSearch() As Task

        Await AddUser()
        Await AddUser()
        Dim user As User = Await AddUser("john")
        Await AddUser()
        ' get
        Dim usersFromDb = Await Repo.GetAllByX(Of User, String)(NameOf(user.Name), user.Name)

        ' asserts
        Assert.Equal(1, usersFromDb.Count)
        Assert.Equal(user, usersFromDb.First())
    End Function
End Class