' this just to clear the db and not really part of the example 
Imports DapperRepoVb.DbItems
Imports Mkb.DapperRepo.Repo

Module CleanUp
    Private Sub DeleteAllOfType(Of T)(repo As SqlRepo)
        Dim items = repo.GetAll(Of T)()
        For Each item As T In items
            repo.Delete(item)
        Next
    End Sub
    
    Private Async Function DeleteAllOfTypeAsync(Of T)(repo As SqlRepoAsync) As Task
        Dim items = Await repo.GetAll(Of T)()
        For Each item As T In items
            Await repo.Delete(item)
        Next
    End Function

    Public Async Function ClearDbAsync(repo As SqlRepoAsync) As Task
        Await DeleteAllOfTypeAsync(Of Post)(repo)
        Await DeleteAllOfTypeAsync(Of User)(repo)
    End Function

    Public Sub ClearDb(repo As SqlRepo)
        DeleteAllOfType(Of Post)(repo)
        DeleteAllOfType(Of User)(repo)
    End Sub
End Module
