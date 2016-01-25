Public Class ConfigBox
    Private Sub Configuracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = Application.ProductName + " - Opciones"

        LoadConfiguration()

    End Sub

    Private Sub LoadConfiguration()
        ' Asigna los datos de la configuracion de conexion a la base de datos
        txtIP.Text = My.Settings.IP
        txtDatabase.Text = My.Settings.Database
        txtUsername.Text = My.Settings.Username
        txtPassword.Text = My.Settings.Password
        ' Asigna los datos de la configuracion del usuario
        txtRazon.Text = My.Settings.Razon
        txtRFC.Text = My.Settings.RFC
        txtTerminal.Text = My.Settings.Terminal

    End Sub

    Private Sub SaveConfiguration()
        ' Almacena los valores de la configuracion de conexion a la base de datos
        My.Settings.IP = txtIP.Text
        My.Settings.Database = txtDatabase.Text
        My.Settings.Username = txtUsername.Text
        My.Settings.Password = txtPassword.Text
        ' Almacena los  valores de la configuracion del usuario
        My.Settings.Razon = txtRazon.Text
        My.Settings.RFC = txtRFC.Text
        My.Settings.Terminal = txtTerminal.Text

    End Sub

    Private Sub btnAcept_Click(sender As Object, e As EventArgs) Handles btnAcept.Click
        Try
            SaveConfiguration()
            ' Guarda los valores
            My.Settings.Save()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Me.Close()

    End Sub
End Class