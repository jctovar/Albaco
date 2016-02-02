Imports MySql.Data.MySqlClient
Public Class ProductDB
    Public Shared Function GetProduct(productID As Integer) As Product
        Dim product As New Product
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT * FROM product WHERE product_id = @product_id"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@product_id", productID)

        Try
            Connection.Open()

            Dim reader As MySqlDataReader = dbcommand.ExecuteReader(CommandBehavior.SingleRow)
            If reader.Read Then
                product.Id = reader("product_id").ToString
                product.Name = reader("product_name").ToString
                product.Category = reader("category_id").ToString
                product.Key = reader("product_key").ToString
                product.Code = reader("product_code").ToString
                product.Type = reader("type_id").ToString
                product.Unit = reader("unit_id").ToString
                product.TareWeight = reader("product_tare_weight").ToString
                product.Description = reader("product_description").ToString
                product.Price1 = reader("product_price_1").ToString
                product.Price2 = reader("product_price_2").ToString
                product.Price3 = reader("product_price_3").ToString
            Else
                product = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try

        Return product
    End Function
    Public Shared Function GetAllProducts() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT product_id, category_name AS 'Categoria', product_name As 'Nombre del producto',product_key AS 'Clave',unit_name AS 'Unidad',CONCAT('$ ',FORMAT(product_price_1,2)) AS 'Precio publico' " &
            "FROM product t1 INNER JOIN category t2 INNER JOIN unit t3 " &
            "ON t1.category_id = t2.category_id AND t1.unit_id = t3.unit_id " &
            "ORDER BY t1.category_id"

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
    Public Shared Function AddProduct(product As Product) As Integer
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "INSERT product " &
            "(category_id,product_name,product_key,product_code,unit_id,type_id,product_tare_weight,product_price_1,product_price_2,product_price_3,product_description) " &
            "VALUES (@category,@name,@key,@code,@unit,@type,@tare,@price1,@price2,@price3,@description);" &
            "SELECT LAST_INSERT_ID()"
        Dim dbcommand = New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@category", product.Category)
        dbcommand.Parameters.AddWithValue("@name", product.Name)
        dbcommand.Parameters.AddWithValue("@key", product.Key)
        dbcommand.Parameters.AddWithValue("@code", product.Code)
        dbcommand.Parameters.AddWithValue("@unit", product.Unit)
        dbcommand.Parameters.AddWithValue("@type", product.Type)
        dbcommand.Parameters.AddWithValue("@tare", product.TareWeight)
        dbcommand.Parameters.AddWithValue("@price1", product.Price1)
        dbcommand.Parameters.AddWithValue("@price2", product.Price2)
        dbcommand.Parameters.AddWithValue("@price3", product.Price3)
        dbcommand.Parameters.AddWithValue("@description", product.Description)

        Try
            Connection.Open()

            Dim cmd_result As Integer = CInt(dbcommand.ExecuteScalar())
            Return cmd_result
        Catch ex As Exception
            Throw ex
            'MessageBox.Show(ex.Message, ex.GetType.ToString)
        Finally
            Connection.Close()
        End Try

    End Function
    Public Shared Function UpdateProduct(product As Product) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "UPDATE product " &
            "SET category_id=@category,product_name=@name,product_key=@key,product_code=@code,unit_id=@unit,type_id=@type,product_tare_weight=@tare,product_price_1=@price1,product_price_2=@price2,product_price_3=@price3,product_description=@description " &
            "WHERE product_id=@id"
        Dim dbcommand = New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", product.Id)
        dbcommand.Parameters.AddWithValue("@category", product.Category)
        dbcommand.Parameters.AddWithValue("@name", product.Name)
        dbcommand.Parameters.AddWithValue("@key", product.Key)
        dbcommand.Parameters.AddWithValue("@code", product.Code)
        dbcommand.Parameters.AddWithValue("@unit", product.Unit)
        dbcommand.Parameters.AddWithValue("@type", product.Type)
        dbcommand.Parameters.AddWithValue("@tare", product.TareWeight)
        dbcommand.Parameters.AddWithValue("@price1", product.Price1)
        dbcommand.Parameters.AddWithValue("@price2", product.Price2)
        dbcommand.Parameters.AddWithValue("@price3", product.Price3)
        dbcommand.Parameters.AddWithValue("@description", product.Description)

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
    Public Shared Function DeleteProduct(product As Product) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "DELETE FROM product " &
            "WHERE product_id=@id"
        Dim dbcommand = New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", product.Id)

        Try
            Connection.Open()

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
    Public Shared Function GetPrices(productID As Integer) As List(Of Price)
        Dim priceList As New List(Of Price)
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT price_product_value,price_id FROM price_product " &
            "WHERE product_id=@id " &
            "ORDER BY price_id"

        Dim dbcommand = New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", productID)

        Try
            Connection.Open()

            Dim reader As MySqlDataReader = dbcommand.ExecuteReader()
            If reader.HasRows Then
                Dim prices As Price
                Do While reader.Read
                    prices = New Price
                    prices.Price = reader("price_product_value").ToString
                    priceList.Add(prices)
                Loop
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try

        Return priceList
    End Function
    Public Shared Function GetCategoriesList() As List(Of Category)
        Dim categoryList As New List(Of Category)
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT category_id,category_name FROM category"

        Dim dbcommand = New MySqlCommand(Sql, Connection)

        Try
            Connection.Open()

            Dim reader As MySqlDataReader = dbcommand.ExecuteReader()
            If reader.HasRows Then
                Dim category As Category
                Do While reader.Read
                    category = New Category
                    category.Id = reader("category_id").ToString
                    category.Name = reader("category_name").ToString
                    categoryList.Add(category)
                Loop
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            Connection.Close()
        End Try

        Return categoryList
    End Function
    Public Shared Function GetUnitsList() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT unit_id,unit_name FROM unit"

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
    Public Shared Function GetTypeList() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT type_id,type_name FROM type"

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
