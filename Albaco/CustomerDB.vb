Imports MySql.Data.MySqlClient

Public Class CustomerDB
    Public Shared Function GetCustomer(customerID As Integer) As Customer
        Dim customer As New Customer
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT * FROM customer WHERE customer_id = @customer_id"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@customer_id", customerID)

        Try
            Connection.Open()

            Dim reader As MySqlDataReader = dbcommand.ExecuteReader(CommandBehavior.SingleRow)
            If reader.Read Then
                customer.Name = reader("customer_name").ToString
                customer.Address_1 = reader("customer_address_1").ToString
                customer.Address_2 = reader("customer_address_2").ToString
                customer.PostalCode = reader("customer_postalcode").ToString
                customer.Phone = reader("customer_phone").ToString
                customer.Email = reader("customer_email").ToString
            Else
                customer = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try

        Return customer
    End Function

    Public Shared Function GetAllCustomers() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT t1.customer_id, t1.customer_name AS 'Nombre', t1.customer_address_1 AS 'Direccion',t1.customer_address_2 AS 'Colonia', t1.customer_phone AS 'Telefono' " &
            "FROM customer t1 " &
            "ORDER BY t1.customer_name"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

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

    Public Shared Function AddCustomer(customer As Customer) As Integer
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "INSERT customer " &
            "(customer_name,customer_address_1,customer_address_2,customer_postalcode,customer_phone,customer_email) " &
            "VALUES (@name,@address_1,@address_2,@postalcode,@phone,@email)"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@name", customer.Name)
        dbcommand.Parameters.AddWithValue("@address_1", customer.Address_1)
        dbcommand.Parameters.AddWithValue("@address_2", customer.Address_2)
        dbcommand.Parameters.AddWithValue("@postalcode", customer.PostalCode)
        dbcommand.Parameters.AddWithValue("@phone", customer.Phone)
        dbcommand.Parameters.AddWithValue("@email", customer.Email)

        Try
            Connection.Open()

            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try
    End Function

    Public Shared Function UpdateCustomer(customer As Customer) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "UPDATE customer " &
            "SET customer_name=@name,customer_address_1=@address_1,customer_address_2=@address_2,customer_postalcode=@postalcode,customer_phone=@phone,customer_email=@email " &
            "WHERE customer_id=@id"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", customer.Id)
        dbcommand.Parameters.AddWithValue("@name", customer.Name)
        dbcommand.Parameters.AddWithValue("@address_1", customer.Address_1)
        dbcommand.Parameters.AddWithValue("@address_2", customer.Address_2)
        dbcommand.Parameters.AddWithValue("@postalcode", customer.PostalCode)
        dbcommand.Parameters.AddWithValue("@phone", customer.Phone)
        dbcommand.Parameters.AddWithValue("@email", customer.Email)

        Try
            Connection.Open()

            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try
    End Function

    Public Shared Function DeleteCustomer(customer As Customer) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "DELETE FROM customer " &
            "WHERE  category_id=@id AND category_name=@name AND category_description=@description "
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", customer.Id)
        dbcommand.Parameters.AddWithValue("@name", customer.Name)
        dbcommand.Parameters.AddWithValue("@address_1", customer.Address_1)
        dbcommand.Parameters.AddWithValue("@address_2", customer.Address_2)
        dbcommand.Parameters.AddWithValue("@postalcode", customer.PostalCode)
        dbcommand.Parameters.AddWithValue("@phone", customer.Phone)
        dbcommand.Parameters.AddWithValue("@email", customer.Email)

        Try
            Connection.Open()

            Dim count As Integer = dbcommand.ExecuteNonQuery()
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            Connection.Close()
        End Try
    End Function
End Class