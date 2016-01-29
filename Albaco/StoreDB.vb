Imports MySql.Data.MySqlClient

Public Class StoreDB
    Public Shared Function GetStore(storeID As Integer) As Store
        Dim store As New Store
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT * FROM store WHERE store_id = @store_id"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@store_id", storeID)

        Try
            Connection.Open()

            Dim reader As MySqlDataReader = dbcommand.ExecuteReader(CommandBehavior.SingleRow)
            If reader.Read Then
                store.Id = reader("store_id").ToString
                store.Name = reader("store_name").ToString
                store.Address = reader("store_address").ToString
                store.Phone = reader("store_phone").ToString
            Else
                store = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try

        Return store
    End Function

    Public Shared Function GetAllStores() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT t1.store_id, t1.store_name AS 'Almacen', t1.store_phone AS 'Telefono', t1.store_address AS 'Direccion' " &
            "FROM store t1"
        Dim dbcommand = New MySqlCommand(Sql, Connection)

        Try
            Connection.Open()

            Dim reader As MySqlDataReader = dbcommand.ExecuteReader()
            If reader.HasRows Then
                dt.Load(reader)
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try

        Return dt
    End Function

    Public Shared Function AddStore(store As Store) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "INSERT store " &
            "(store_name,store_address,store_phone,company_id) " &
            "VALUES (@name,@address,@phone,@company)"
        Dim dbcommand = New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@name", store.Name)
        dbcommand.Parameters.AddWithValue("@address", store.Address)
        dbcommand.Parameters.AddWithValue("@phone", store.Phone)
        dbcommand.Parameters.AddWithValue("@company", 1)

        Try
            dbcommand.Connection.Open()

            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try

    End Function

    Public Shared Function UpdateStore(store As Store) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "UPDATE store " &
            "SET store_name=@name,store_address=@address,store_phone=@phone " &
            "WHERE store_id=@id"
        Dim dbcommand = New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", store.Id)
        dbcommand.Parameters.AddWithValue("@name", store.Name)
        dbcommand.Parameters.AddWithValue("@address", store.Address)
        dbcommand.Parameters.AddWithValue("@phone", store.Phone)

        Try
            dbcommand.Connection.Open()

            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try

    End Function

    Public Shared Function DeleteStore(store As Store) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "DELETE FROM store " &
            "WHERE  store_id=@id AND store_name=@name AND store_address=@address AND store_phone=@phone"
        Dim dbcommand = New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", store.Id)
        dbcommand.Parameters.AddWithValue("@name", store.Name)
        dbcommand.Parameters.AddWithValue("@address", store.Address)
        dbcommand.Parameters.AddWithValue("@phone", store.Phone)

        Try
            dbcommand.Connection.Open()

            Dim count As Integer = dbcommand.ExecuteNonQuery()
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try
    End Function
End Class
