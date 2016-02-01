Public Class ProductSearch
    Private Sub ProductSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = String.Format("{0} - {1}", Application.ProductName, "Catalogo de Productos")

        Me.FillDatagrid()
    End Sub
    Public Sub FillDatagrid()
        Dim tableview As New DataTable

        tableview = ProductDB.GetAllProducts

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
    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        Me.UpdateProduct()
    End Sub

    Private Sub DataGridView1_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            'MsgBox("You have performed Left Mouse Click")
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip1.Show(Windows.Forms.Cursor.Position.X, Windows.Forms.Cursor.Position.Y)
        End If
    End Sub
    Private Sub AddProduct()
        Dim frmProduct As New ProductBox

        frmProduct.addProduct = True

        Dim result As DialogResult = frmProduct.ShowDialog()

        If result = DialogResult.OK Then
            FillDatagrid()
        End If
    End Sub
    Private Sub UpdateProduct()
        Dim frmProduct As New ProductBox

        frmProduct.addProduct = False
        frmProduct.productID = DataGridView1(0, DataGridView1.CurrentRow.Index).Value

        Dim result As DialogResult = frmProduct.ShowDialog()

        If result = DialogResult.OK Then
            FillDatagrid()
        End If
    End Sub
    Private Sub DeleteProduct()
        If MessageBox.Show("Esta seguro de eliminar el registro?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            Dim product As New Product

            product = ProductDB.GetProduct(DataGridView1(0, DataGridView1.CurrentRow.Index).Value)

            Dim result As Boolean = ProductDB.DeleteProduct(product)

            If result Then
                FillDatagrid()
            Else
                MessageBox.Show("Ocurrio un error o el registro ya no existe!", Application.ProductName)
            End If
        End If
    End Sub
    Private Sub EditarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditarToolStripMenuItem.Click
        Me.UpdateProduct()
    End Sub
    Private Sub CerrarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CerrarToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub NuevoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoToolStripMenuItem.Click
        Me.AddProduct()
    End Sub
    Private Sub EditarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditarToolStripMenuItem1.Click
        Me.UpdateProduct()
    End Sub
    Private Sub EliminarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem1.Click
        Me.DeleteProduct()
    End Sub
    Private Sub NuevoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NuevoToolStripMenuItem1.Click
        Me.AddProduct()
    End Sub
    Private Sub CerrarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CerrarToolStripMenuItem1.Click
        Me.Close()
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyValue = Keys.Space Then
            Me.UpdateProduct()
        End If
    End Sub
End Class