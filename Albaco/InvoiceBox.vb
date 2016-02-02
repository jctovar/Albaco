Public Class InvoiceBox
    Public addProduct As Boolean
    Public invoiceID As Integer
    Private Sub InvoiceBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0} - {1}", Application.ProductName, "Venta")

        FillDatagrid()
    End Sub
    Public Sub FillDatagrid()
        Dim tableview As New DataTable

        tableview = InvoiceDB.GetAllProducts(invoiceID)

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor


            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = tableview
            DataGridView1.AutoResizeColumns()
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Refresh()
        Catch ex As Exception

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub
End Class