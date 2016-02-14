Imports MySql.Data.MySqlClient
Public Class InvoiceDB
    Public Shared Function GetInvoice(invoiceID As Integer) As Invoice
        Dim invoice As New Invoice
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT t1.invoice_id,t1.account_id,t1.customer_id,t1.status_id,t1.invoice_timestamp,CONCAT('$ ',FORMAT(SUM(t2.invoice_product_quantity*t2.invoice_product_price),2)) as total " &
            "FROM invoice t1 INNER JOIN invoice_product t2 ON t1.invoice_id=t2.invoice_id WHERE t1.invoice_id = @id"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", invoiceID)

        Try
            Connection.Open()

            Dim reader As MySqlDataReader = dbcommand.ExecuteReader(CommandBehavior.SingleRow)

            If reader.Read Then
                invoice.Id = reader("invoice_id").ToString
                invoice.Customer = reader("customer_id").ToString
                invoice.Account = reader("account_id").ToString
                invoice.Status = reader("status_id").ToString
                invoice.Total = reader("total").ToString
                invoice.Timestamp = reader("invoice_timestamp").ToString
            Else
                invoice = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try

        Return invoice
    End Function
    Public Shared Function GetAllInvoices() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT LPAD(t1.invoice_id,10,'0') AS 'Folio',customer_name AS 'Cliente', account_name as 'Vendedor', status_id as 'Estado', DATE_FORMAT(invoice_timestamp,'%b %d %Y %h:%i %p') AS 'Fecha de venta',CONCAT('$ ',FORMAT(SUM(invoice_product_price*invoice_product_quantity),2)) AS Total " &
            "FROM invoice t1 INNER JOIN customer t2 INNER JOIN account t3 INNER JOIN invoice_product t4 " &
            "ON t1.customer_id = t2.customer_id AND t1.account_id = t3.account_id AND t1.invoice_id=t4.invoice_id " &
            "GROUP BY t1.invoice_id " &
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
    Public Shared Function GetAllProductsOfInvoice(invoiceID As Integer) As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT t1.invoice_id,t1.product_id,product_name AS 'Producto',CONCAT('$ ',FORMAT(invoice_product_price,2)) AS 'Precio unitario',CONCAT(invoice_product_quantity,' ',unit_name) AS 'Cantidad',CONCAT('$ ',FORMAT((invoice_product_price*invoice_product_quantity),2)) AS 'Subtotal' " &
            "FROM invoice_product t1 INNER JOIN product t2 INNER JOIN unit t3 " &
            "ON t1.product_id = t2.product_id AND t2.unit_id=t3.unit_id " &
            "WHERE invoice_id=@id " &
            "ORDER BY category_id DESC"
        Dim dbcommand = New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", invoiceID)

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
    Public Shared Function AddProductToInvoice(invoiceID As Integer, productID As Integer, productQty As Double, productPrice As Double) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "INSERT invoice_product " &
            "(invoice_id,product_id,invoice_product_quantity,invoice_product_price) " &
            "VALUES (@invoice,@product,@quantity,@price)"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@invoice", invoiceID)
        dbcommand.Parameters.AddWithValue("@product", productID)
        dbcommand.Parameters.AddWithValue("@quantity", productQty)
        dbcommand.Parameters.AddWithValue("@price", productPrice)

        Try
            Connection.Open()

            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
            ' Throw ex
        Finally
            Connection.Close()
        End Try
    End Function
    Public Shared Function DeleteProductForomInvoice(invoiceID As Integer, productID As Integer) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "DELETE FROM invoice_product " &
            "WHERE invoice_id=@invoice AND product_id=@product"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@invoice", invoiceID)
        dbcommand.Parameters.AddWithValue("@product", productID)

        Try
            Connection.Open()

            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.GetType.ToString)
            ' Throw ex
        Finally
            Connection.Close()
        End Try
    End Function
    Public Shared Function GetProductsList() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT product_id,product_name FROM product ORDER BY product_name"

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
    Public Shared Function GetCustomersList() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT customer_id,customer_name FROM customer ORDER BY customer_name"

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
End Class
