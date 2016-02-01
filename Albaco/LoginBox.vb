Public Class LoginBox
    Private Sub LoginBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0} - {1}", Application.ProductName, My.Settings.Razon)

        'Título de la aplicación
         If My.Application.Info.Title <> "" Then
            ApplicationTitle.Text = My.Application.Info.Title
        Else
            'Si falta el título de la aplicación, utilice el nombre de la aplicación sin la extensión
            ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If

        If System.Diagnostics.Debugger.IsAttached = False Then
            Me.Version.Text = String.Format("Versión {0}", My.Application.Deployment.CurrentVersion.ToString)
        Else
            Me.Version.Text = String.Format("Versión {0}", "Debug mode")
        End If

        'Información de Copyright
        Copyright.Text = My.Application.Info.Copyright
    End Sub
    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        Me.Config()

        Me.DialogResult = DialogResult.OK

    End Sub
    Private Sub Config()

        My.Settings.IP = "104.131.131.243"
        My.Settings.Database = "albaco"
        My.Settings.Username = "albaco"
        My.Settings.Password = "5Yh8d-6+9fCG5Q"

        My.Settings.Save()
    End Sub
End Class