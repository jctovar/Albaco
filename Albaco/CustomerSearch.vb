Public Class CustomerSearch
    Private Sub CustomerSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0} - {1}", Application.ProductName, "Catalogo de clientes")

        Me.FillDatagrid()
    End Sub
    Public Sub FillDatagrid()
        Dim tableview As New DataTable

        tableview = CustomerDB.GetAllCustomers

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            txtStatus.Text = String.Format("Se encontraron {0} registros", tableview.Rows.Count)

            DataGridView1.AutoGenerateColumns = True
            DataGridView1.DataSource = tableview
            DataGridView1.AutoResizeColumns()
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Refresh()
        Catch ex As Exception
            txtStatus.Text = String.Format("Se encontraron {0} registros", tableview.Rows.Count)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Sub AddCustomer()
        Dim frmCustomer As New CustomerBox

        frmCustomer.addCustomer = True

        Dim result As DialogResult = frmCustomer.ShowDialog()

        If result = DialogResult.OK Then
            FillDatagrid()
        End If
    End Sub
    Private Sub UpdateCustomer()
        Dim frmCustomer As New CustomerBox

        frmCustomer.addCustomer = False
        frmCustomer.customerID = DataGridView1(0, DataGridView1.CurrentRow.Index).Value

        Dim result As DialogResult = frmCustomer.ShowDialog()

        If result = DialogResult.OK Then
            FillDatagrid()
        End If
    End Sub
    Private Sub DeleteCustomer()
        If MessageBox.Show("Esta seguro de eliminar el registro?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            Dim customer As New Customer

            customer = CustomerDB.GetCustomer(DataGridView1(0, DataGridView1.CurrentRow.Index).Value)

            Dim result As Boolean = CustomerDB.DeleteCustomer(customer)

            If result Then
                FillDatagrid()
            Else
                MessageBox.Show("Ocurrio un error o el registro ya no existe!", Application.ProductName)
            End If
        End If
    End Sub
    Private Sub EditarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditarToolStripMenuItem1.Click
        Me.UpdateCustomer()
    End Sub
    Private Sub EliminarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem1.Click
        Me.DeleteCustomer()
    End Sub
    Private Sub NuevoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NuevoToolStripMenuItem1.Click
        Me.AddCustomer()
    End Sub
    Private Sub CerrarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CerrarToolStripMenuItem1.Click
        Me.Close()
    End Sub
    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Me.UpdateCustomer()
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyValue = Keys.Space Then
            Me.UpdateCustomer()
        End If
    End Sub
End Class