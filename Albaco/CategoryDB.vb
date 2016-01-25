Imports MySql.Data.MySqlClient

Public Class CategoryDB
    Public Shared Function GetCategory(categoryID As Integer) As Category
        Dim Category As New Category
        Dim Sql As String = "SELECT * FROM caja.categories WHERE category_id=@id"
        Dim dbcommand As New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", categoryID)

        Try
            Dim reader As MySqlDataReader = dbcommand.ExecuteReader(CommandBehavior.SingleRow)
            If reader.Read Then
                Category.Id = reader("category_id").ToString
                Category.Name = reader("category_name").ToString
                Category.Description = reader("category_description").ToString
            Else
                Category = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        End Try

        Return Category
    End Function

    Public Shared Function GetAllCategories() As DataTable
        Dim dt = New DataTable()
        Dim Sql As String = "SELECT t1.category_id, t1.category_name AS 'Categoria', t1.category_description AS 'Descripcion' " &
            "FROM caja.categories t1"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        Try
            Dim reader As MySqlDataReader = dbcommand.ExecuteReader()
            If reader.HasRows Then
                dt.Load(reader)
            Else
                reader = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try

        Return dt
    End Function

    Public Shared Function AddCategory(category As Category) As Integer
        Dim Sql As String = "INSERT categories " &
            "(category_name,category_description) " &
            "VALUES (@name,@description)"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@name", category.Name)
        dbcommand.Parameters.AddWithValue("@description", category.Description)

        Try
            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try
    End Function

    Public Shared Function UpdateCategory(category As Category) As Boolean
        Dim Sql As String = "UPDATE categories " &
            "SET category_name=@name,category_description=@description " &
            "WHERE category_id=@id"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", category.Id)
        dbcommand.Parameters.AddWithValue("@name", category.Name)
        dbcommand.Parameters.AddWithValue("@description", category.Description)

        Try
            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try
    End Function

    Public Shared Function DeleteCategory(category As Category) As Boolean
        Dim Sql As String = "DELETE FROM categories " &
            "WHERE  category_id=@id AND category_name=@name AND category_description=@description "
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", category.Id)
        dbcommand.Parameters.AddWithValue("@name", category.Name)
        dbcommand.Parameters.AddWithValue("@description", category.Description)

        Try
            Dim count As Integer = dbcommand.ExecuteNonQuery()
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try
    End Function
End Class
