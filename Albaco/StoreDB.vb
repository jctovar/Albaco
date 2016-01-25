Imports MySql.Data.MySqlClient

Public Class StoreDB
    Public Shared Function GetStore(storeID As Integer) As Store
        Dim store As New Store
        Dim Sql As String = "SELECT * FROM caja.stores WHERE store_id = @store_id"
        Dim dbcommand As New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@store_id", storeID)

        Try
            Dim reader As MySqlDataReader = dbcommand.ExecuteReader(CommandBehavior.SingleRow)
            If reader.Read Then
                store.ID = reader("store_id").ToString
                store.Name = reader("store_name").ToString
                store.Address = reader("store_address").ToString
                store.Phone = reader("store_phone").ToString
            Else
                store = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        End Try

        Return store
    End Function

    Public Shared Function GetAllStores() As DataTable
        Dim dt = New DataTable()
        Dim Sql As String = "SELECT t1.store_id, t1.store_name AS 'Almacen', t1.store_phone AS 'Telefono', t1.store_address AS 'Direccion' " &
            "FROM store t1"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        Try
            Dim reader As MySqlDataReader = dbcommand.ExecuteReader()
            If reader.HasRows Then
                dt.Load(reader)
            Else
                dt = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try

        Return dt
    End Function

    Public Shared Function AddStore(store As Store) As Integer
        Dim Sql As String = "INSERT stores " &
            "(store_name,store_address,store_phone) " &
            "VALUES (@name,@address,@phone)"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@name", store.Name)
        dbcommand.Parameters.AddWithValue("@address", store.Address)
        dbcommand.Parameters.AddWithValue("@phone", store.Phone)

        Try
            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try

    End Function

    Public Shared Function UpdateStore(store As Store) As Boolean
        Dim Sql As String = "UPDATE stores " &
            "SET store_name=@name,store_address=@address,store_phone=@phone " &
            "WHERE store_id=@id"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", store.ID)
        dbcommand.Parameters.AddWithValue("@name", store.Name)
        dbcommand.Parameters.AddWithValue("@address", store.Address)
        dbcommand.Parameters.AddWithValue("@phone", store.Phone)

        Try
            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try

    End Function

    Public Shared Function DeleteStore(store As Store) As Boolean
        Dim Sql As String = "DELETE FROM stores " &
            "WHERE  store_id=@id AND store_name=@name AND store_address=@address AND store_phone=@phone"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", store.ID)
        dbcommand.Parameters.AddWithValue("@name", store.Name)
        dbcommand.Parameters.AddWithValue("@address", store.Address)
        dbcommand.Parameters.AddWithValue("@phone", store.Phone)

        Try
            Dim count As Integer = dbcommand.ExecuteNonQuery()
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try
    End Function
End Class
