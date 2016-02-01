Imports MySql.Data.MySqlClient
Public Class UserDB
    Public Shared Function GetUser(userID As Integer) As User
        Dim user As New User
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT * FROM account WHERE account_id = @account_id"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@account_id", userID)

        Try
            Connection.Open()

            Dim reader As MySqlDataReader = dbcommand.ExecuteReader(CommandBehavior.SingleRow)
            If reader.Read Then
                user.Name = reader("account_name").ToString
                user.Username = reader("account_username").ToString
                user.Password = reader("account_password").ToString
                user.Phone = reader("account_phone").ToString
                user.Email = reader("account_email").ToString
                user.Status = reader("account_enable").ToString
                user.Profile = reader("rol_id").ToString
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
    Public Shared Function GetAllUsers() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT t1.account_id,t1.account_username AS 'Usuario',t1.account_name AS 'Nombre completo',t1.account_enable AS 'Estado',t2.rol_name AS 'Perfil' " &
            "FROM account t1 INNER JOIN rol t2 " &
            "ON t1.rol_id=t2.rol_id " &
            "ORDER BY t1.account_username"
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
    Public Shared Function AddUser(user As User) As Integer
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "INSERT account " &
            "(account_name,account_username,account_password,account_phone,account_email,account_enable,rol_id) " &
            "VALUES (@name,@username,@password,@phone,@email,@enable,@rol)"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@name", user.Name)
        dbcommand.Parameters.AddWithValue("@username", user.Username)
        dbcommand.Parameters.AddWithValue("@password", user.Password)
        dbcommand.Parameters.AddWithValue("@phone", user.Phone)
        dbcommand.Parameters.AddWithValue("@email", user.Email)
        dbcommand.Parameters.AddWithValue("@enable", user.Status)
        dbcommand.Parameters.AddWithValue("@rol", user.Profile)

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
    Public Shared Function UpdateUsere(user As User) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "UPDATE account " &
            "SET account_name=@name,account_username=@username,account_phone=@phone,account_email=@email,account_enable=@enable,rol_id=@rol " &
            "WHERE account_id=@id"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", user.Id)
        dbcommand.Parameters.AddWithValue("@name", user.Name)
        dbcommand.Parameters.AddWithValue("@username", user.Username)
        dbcommand.Parameters.AddWithValue("@password", user.Password)
        dbcommand.Parameters.AddWithValue("@phone", user.Phone)
        dbcommand.Parameters.AddWithValue("@email", user.Email)
        dbcommand.Parameters.AddWithValue("@enable", user.Status)
        dbcommand.Parameters.AddWithValue("@rol", user.Profile)

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
    Public Shared Function DeleteUser(user As User) As Boolean
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "DELETE FROM account " &
            "WHERE  account_id=@id AND account_name=@name AND account_username=@address AND account_phone=@phone"
        Dim dbcommand As New MySqlCommand(Sql, Connection)

        dbcommand.Parameters.AddWithValue("@id", user.Id)
        dbcommand.Parameters.AddWithValue("@name", user.Name)
        dbcommand.Parameters.AddWithValue("@username", user.Username)
        dbcommand.Parameters.AddWithValue("@password", "12345678")
        dbcommand.Parameters.AddWithValue("@phone", user.Phone)
        dbcommand.Parameters.AddWithValue("@email", user.Email)
        dbcommand.Parameters.AddWithValue("@enable", user.Status)

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
    Public Shared Function GetProfileList() As DataTable
        Dim dt = New DataTable()
        Dim Connection As MySqlConnection = MySqlDataBase.GetConnection
        Dim Sql As String = "SELECT rol_id,rol_description FROM rol"

        Dim dbcommand = New MySqlCommand(Sql, Connection)

        Try
            Connection.Open()

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
            Connection.Close()
        End Try

        Return dt
    End Function
    Public Shared Function GetEnableList() As DataTable
        Dim dt = New DataTable()
        Dim dr As DataRow
        Dim idCoulumn As DataColumn
        Dim nameCoulumn As DataColumn

        idCoulumn = New DataColumn("status_id", Type.GetType("System.Int32"))
        nameCoulumn = New DataColumn("status_name", Type.GetType("System.String"))

        dt.Columns.Add(idCoulumn)
        dt.Columns.Add(nameCoulumn)

        dr = dt.NewRow()
        dr("status_id") = 0
        dr("status_name") = "Deshabilitado"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("status_id") = 1
        dr("status_name") = "Habilitado"
        dt.Rows.Add(dr)

        Return dt
    End Function
End Class
