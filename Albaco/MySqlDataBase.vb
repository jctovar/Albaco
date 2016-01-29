Imports MySql.Data.MySqlClient

Public Class MySqlDataBase
    Public Shared Function GetConnection() As MySqlConnection
        Dim dbconn As New MySqlConnection
        Dim DatabaseName As String = My.Settings.Database
        Dim ServerIP As String = My.Settings.IP
        Dim Username As String = My.Settings.Username
        Dim Password As String = My.Settings.Password

        If Not dbconn Is Nothing Then dbconn.Close()
        Try
            ' Arma la cadena de conexion
            dbconn.ConnectionString = String.Format("server={0}; user id={1}; password={2}; database={3}; UseCompression=true", ServerIP, Username, Password, DatabaseName)
        Catch ex As Exception
            Throw ex
        End Try

        Return dbconn
    End Function

    Public Shared Function NetworkStatus() As Boolean
        If My.Computer.Network.Ping(My.Settings.IP) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
