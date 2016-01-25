Public Class InvoiceBox
    Private Sub InvoiceBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0} - {1}", Application.ProductName, "Venta")
    End Sub
End Class