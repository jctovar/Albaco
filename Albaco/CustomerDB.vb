Imports MySql.Data.MySqlClient

Public Class CustomerDB
    Public Shared Function GetCustomer(customerID As Integer) As Customer
        Dim customer1 As New Customer
        Dim Sql As String = "SELECT * FROM caja.customers WHERE customer_id = @customer_id"
        Dim dbcommand As New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@customer_id", customerID)

        Try
            Dim reader As MySqlDataReader = dbcommand.ExecuteReader(CommandBehavior.SingleRow)
            If reader.Read Then
                customer1.Name = reader("customer_name").ToString
                customer1.Address_1 = reader("customer_address_1").ToString
                customer1.Address_2 = reader("customer_address_2").ToString
                customer1.PostalCode = reader("customer_postalcode").ToString
                customer1.Phone = reader("customer_phone").ToString
                customer1.Email = reader("customer_email").ToString
            Else
                customer1 = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        End Try

        Return customer1
    End Function

    Public Shared Function GetAllCustomers() As DataTable
        Dim dt = New DataTable()
        Dim Sql As String = "SELECT t1.customer_id, t1.customer_name AS 'Nombre', t1.customer_address_1 AS 'Direccion',t1.customer_address_2 AS 'Colonia', t1.customer_phone AS 'Telefono' " &
            "FROM caja.customers t1 " &
            "ORDER BY t1.customer_name"
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

    Public Shared Function AddCustomer(customer As Customer) As Integer
        Dim Sql As String = "INSERT customers " &
            "(customer_name,customer_address_1,customer_address_2,customer_postalcode,customer_phone,customer_email) " &
            "VALUES (@name,@address_1,@address_2,@postalcode,@phone,@email)"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@name", customer.Name)
        dbcommand.Parameters.AddWithValue("@address_1", customer.Address_1)
        dbcommand.Parameters.AddWithValue("@address_2", customer.Address_2)
        dbcommand.Parameters.AddWithValue("@postalcode", customer.PostalCode)
        dbcommand.Parameters.AddWithValue("@phone", customer.Phone)
        dbcommand.Parameters.AddWithValue("@email", customer.Email)

        Try
            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try
    End Function

    Public Shared Function UpdateCustomer(customer As Customer) As Boolean
        Dim Sql As String = "UPDATE customers " &
            "SET category_name=@name,category_description=@description " &
            "WHERE category_id=@id"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", customer.Id)
        dbcommand.Parameters.AddWithValue("@name", customer.Name)
        dbcommand.Parameters.AddWithValue("@address_1", customer.Address_1)
        dbcommand.Parameters.AddWithValue("@address_2", customer.Address_2)
        dbcommand.Parameters.AddWithValue("@postalcode", customer.PostalCode)
        dbcommand.Parameters.AddWithValue("@phone", customer.Phone)
        dbcommand.Parameters.AddWithValue("@email", customer.Email)

        Try
            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try
    End Function

    Public Shared Function DeleteCustomer(customer As Customer) As Boolean
        Dim Sql As String = "DELETE FROM customers " &
            "WHERE  category_id=@id AND category_name=@name AND category_description=@description "
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", customer.Id)
        dbcommand.Parameters.AddWithValue("@name", customer.Name)
        dbcommand.Parameters.AddWithValue("@address_1", customer.Address_1)
        dbcommand.Parameters.AddWithValue("@address_2", customer.Address_2)
        dbcommand.Parameters.AddWithValue("@postalcode", customer.PostalCode)
        dbcommand.Parameters.AddWithValue("@phone", customer.Phone)
        dbcommand.Parameters.AddWithValue("@email", customer.Email)

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