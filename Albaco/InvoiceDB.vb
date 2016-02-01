Imports MySql.Data.MySqlClient
Public Class InvoiceDB
    Public Shared Function GetInvoice(invoiceID As Integer) As User
        Dim user As New User
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT * FROM caja.accounts WHERE account_id = @account_id"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@account_id", invoiceID)

        Try
            Connection.Open()

            Dim reader As MySqlDataReader = dbcommand.ExecuteReader(CommandBehavior.SingleRow)
            If reader.Read Then
                user.Name = reader("account_name").ToString
                user.Username = reader("account_username").ToString
                user.Password = reader("account_password").ToString
                user.Phone = reader("account_phone").ToString
                user.Email = reader("account_email").ToString
            Else
                user = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try

        Return user
    End Function
    Public Shared Function GetAllInvoices() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT LPAD(invoice_id,10,'0') AS 'Folio',customer_name AS 'Cliente', account_name as 'Vendedor', status_id as 'Estado', DATE_FORMAT(invoice_timestamp,'%b %d %Y %h:%i %p') AS 'Fecha de venta' " &
            "FROM invoice t1 INNER JOIN customer t2 INNER JOIN account t3 " &
            "ON t1.customer_id = t2.customer_id AND t1.account_id = t3.account_id " &
            "ORDER BY invoice_timestamp DESC"
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

    Public Shared Function AddInvoice(category As Category) As Integer
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

    Public Shared Function UpdateInvoice(category As Category) As Boolean
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

    Public Shared Function DeleteInvoicey(category As Category) As Boolean
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
