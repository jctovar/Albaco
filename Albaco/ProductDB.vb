Imports MySql.Data.MySqlClient

Public Class ProductDB
    Public Shared Function GetProduct(productID As Integer) As Product
        Dim product As New Product
        Dim Sql As String = "SELECT * FROM caja.products WHERE product_id = @product_id"
        Dim dbcommand As New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@product_id", productID)

        Try
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
                'product.PriceSell = reader("product_price").ToString
            Else
                product = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        End Try

        Return product
    End Function

    Public Shared Function GetAllProducts() As DataTable
        Dim dt = New DataTable()
        Dim Sql As String = "SELECT product_id, category_name AS 'Categoria', product_name As 'Nombre del producto',product_key AS 'Clave',unit_name AS 'Unidad' " &
            "FROM caja.products t1 INNER JOIN caja.categories t2 INNER JOIN caja.units t3 " &
            "ON t1.category_id = t2.category_id AND t1.unit_id = t3.unit_id " &
            "ORDER BY t1.category_id"

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

    Public Shared Function AddProduct(product As Product) As Integer
        Dim Sql As String = "INSERT products " &
            "(category_id,product_name,product_key,product_code,unit_id,type_id,product_tare_weight,product_description,product_price) " &
            "VALUES (@category,@name,@key,@code,@unit,@type,@tare,@description,@price)"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@category", product.Category)
        dbcommand.Parameters.AddWithValue("@name", product.Name)
        dbcommand.Parameters.AddWithValue("@key", product.Key)
        dbcommand.Parameters.AddWithValue("@code", product.Code)
        dbcommand.Parameters.AddWithValue("@unit", product.Unit)
        dbcommand.Parameters.AddWithValue("@type", product.Type)
        dbcommand.Parameters.AddWithValue("@tare", product.TareWeight)
        dbcommand.Parameters.AddWithValue("@description", product.Description)
        'dbcommand.Parameters.AddWithValue("@price", product.PriceSell)

        Try
            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try

    End Function

    Public Shared Function UpdateProduct(product As Product) As Boolean
        Dim Sql As String = "UPDATE products " &
            "SET category_id=@category,product_name=@name,product_key=@key,product_code=@code,unit_id=@unit,type_id=@type,product_tare_weight=@tare,product_description=@description,product_price=@price " &
            "WHERE product_id=@id"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", product.Id)
        dbcommand.Parameters.AddWithValue("@category", product.Category)
        dbcommand.Parameters.AddWithValue("@name", product.Name)
        dbcommand.Parameters.AddWithValue("@key", product.Key)
        dbcommand.Parameters.AddWithValue("@code", product.Code)
        dbcommand.Parameters.AddWithValue("@unit", product.Unit)
        dbcommand.Parameters.AddWithValue("@type", product.Type)
        dbcommand.Parameters.AddWithValue("@tare", product.TareWeight)
        dbcommand.Parameters.AddWithValue("@description", product.Description)
        'dbcommand.Parameters.AddWithValue("@price", product.PriceSell)

        Try
            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try

    End Function

    Public Shared Function DeleteProduct(product As Product) As Boolean
        Dim Sql As String = "DELETE FROM products " &
            "WHERE product_id=@id"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)
        MsgBox(product.Id)
        dbcommand.Parameters.AddWithValue("@id", product.Id)

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

    Public Shared Function GetPrices(productID As Integer) As List(Of Price)
        Dim priceList As New List(Of Price)
        Dim Sql As String = "SELECT price_value FROM price " &
            "WHERE product_id=@id " &
            "ORDER BY price_value"

        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", productID)

        Try
            Dim reader As MySqlDataReader = dbcommand.ExecuteReader()
            If reader.HasRows Then
                Dim prices As Price
                Do While reader.Read
                    prices = New Price
                    prices.Price = reader("price_value").ToString
                    priceList.Add(prices)
                Loop
            Else
                reader = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try

        Return priceList
    End Function

    Public Shared Function GetCategoriesList() As List(Of Category)
        Dim categoryList As New List(Of Category)
        Dim Sql As String = "SELECT category_id,category_name FROM categories"

        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        Try
            Dim reader As MySqlDataReader = dbcommand.ExecuteReader()
            If reader.HasRows Then
                Dim category As Category
                Do While reader.Read
                    category = New Category
                    category.Id = reader("category_id").ToString
                    category.Name = reader("category_name").ToString
                    categoryList.Add(category)
                Loop
            Else
                reader = Nothing
            End If
            reader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try

        Return categoryList
    End Function

    Public Shared Function GetUnitsLists() As DataTable
        Dim dt = New DataTable()
        Dim Sql As String = "SELECT unit_id,unit_name FROM units"

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

    Public Shared Function GetTypeLists() As DataTable
        Dim dt = New DataTable()
        Dim Sql As String = "SELECT type_id,type_name FROM types"

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
End Class
