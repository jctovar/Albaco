Imports MySql.Data.MySqlClient

Public Class UserDB
    Public Shared Function GetUser(userID As Integer) As User
        Dim user As New User
        Dim Sql As String = "SELECT * FROM caja.accounts WHERE account_id = @account_id"
        Dim dbcommand As New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@account_id", userID)

        Try
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
        End Try

        Return user
    End Function

    Public Shared Function GetAllUsers() As DataTable
        Dim dt = New DataTable()
        Dim Sql As String = "SELECT t1.account_id, t1.account_username AS 'Usuario', t1.account_name AS 'Nombre completo', t1.account_phone AS 'Telefono' " &
            "FROM caja.accounts t1 " &
            "ORDER BY t1.account_username"
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

    Public Shared Function AddUser(user As User) As Integer
        Dim Sql As String = "INSERT accounts " &
            "(account_name,account_username,account_password,account_phone,account_email,account_enable) " &
            "VALUES (@name,@username,@password,@phone,@email,@enable)"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@name", user.Name)
        dbcommand.Parameters.AddWithValue("@username", user.Username)
        dbcommand.Parameters.AddWithValue("@password", "12345678")
        dbcommand.Parameters.AddWithValue("@phone", user.Phone)
        dbcommand.Parameters.AddWithValue("@email", user.Email)
        dbcommand.Parameters.AddWithValue("@enable", 1)

        Try
            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try

    End Function

    Public Shared Function UpdateUsere(user As User) As Boolean
        Dim Sql As String = "UPDATE accounts " &
            "SET account_name=@name,account_username=@username,account_phone=@phone,account_email=@email " &
            "WHERE account_id=@id"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", user.Id)
        dbcommand.Parameters.AddWithValue("@name", user.Name)
        dbcommand.Parameters.AddWithValue("@username", user.Username)
        dbcommand.Parameters.AddWithValue("@password", "12345678")
        dbcommand.Parameters.AddWithValue("@phone", user.Phone)
        dbcommand.Parameters.AddWithValue("@email", user.Email)
        dbcommand.Parameters.AddWithValue("@enable", 1)

        Try
            dbcommand.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            MySqlDataBase.GetConnection.Close()
        End Try

    End Function

    Public Shared Function DeleteUser(user As User) As Boolean
        Dim Sql As String = "DELETE FROM accounts " &
            "WHERE  account_id=@id AND account_name=@name AND account_username=@address AND account_phone=@phone"
        Dim dbcommand = New MySqlCommand(Sql, MySqlDataBase.GetConnection)

        dbcommand.Parameters.AddWithValue("@id", user.Id)
        dbcommand.Parameters.AddWithValue("@name", user.Name)
        dbcommand.Parameters.AddWithValue("@username", user.Username)
        dbcommand.Parameters.AddWithValue("@password", "12345678")
        dbcommand.Parameters.AddWithValue("@phone", user.Phone)
        dbcommand.Parameters.AddWithValue("@email", user.Email)
        dbcommand.Parameters.AddWithValue("@enable", 1)

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
