Imports Xunit

Namespace Tests

    Public Class DeleteTests
        Inherits BaseDbTestClass

        <Fact>
        Async Function EnsureWeCanDelete() As Task
            Await AddUser()
            Dim user = Await GetFirstUser()
            Assert.NotNull(user)
            ' delete
            Await Repo.Delete(user)

            ' asserts
            user = Await GetFirstUser()
            Assert.Null(user)
        End Function
    End Class
End NameSpace