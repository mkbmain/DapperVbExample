Imports DapperRepoVb.DbItems
Imports Tests.Tests
Imports Xunit
Imports Dapper

Public Class DeleteTests
    Inherits BaseDbTestClass

    <Fact>
    Async Function EnsureWeCanDelete() As Task
        Await AddUser()
        Dim User = Await GetFirstUser()
        Assert.NotNull(User)
        ' delete
        Await Repo.Delete(User)

        ' asserts
        User = Await GetFirstUser()
        Assert.Null(User)
    End Function
End Class