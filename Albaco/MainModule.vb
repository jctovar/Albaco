Module MainModule
    Sub Main()
        Dim frmLoginBox As New LoginBox

        Application.EnableVisualStyles()
        If frmLoginBox.ShowDialog = DialogResult.OK Then
            Try
                Application.Run(InvoicesAdmin)
            Catch ex As Exception

            End Try
        End If

    End Sub
End Module